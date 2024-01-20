using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.LogModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SuperLotoManager : ISuperLotoService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly ISuperLotoLinks _links;
        private readonly UserManager<User> _userManager;
        private readonly ICacheService _cache;

        public SuperLotoManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, ISuperLotoLinks links, UserManager<User> userManager, ICacheService cache)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _links = links;
            _userManager = userManager;
            _cache = cache;
        }

        public async Task<SuperLotoDto> CreateOneNumbersArrayAsync(SuperLotoDtoForInsertion superLotoDto)
        {
            var entity = _mapper.Map<SuperLoto>(superLotoDto);
            _manager.SuperLoto.CreateOneNumbersArray(entity);
            await _manager.SaveAsync();
            SetCache<SuperLoto>($"superloto-entity-{entity.Id}", entity);
            return _mapper.Map<SuperLotoDto>(entity);
        }

        public async Task DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            _manager.SuperLoto.DeleteOneNumbersArray(entity);
            _cache.RemoveData($"superloto-entity-{id}");
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<SuperLotoParameters> linkParameters, bool trackChanges)
        {
            var page = _cache.GetData<LinkParametersDtoForCache>("superloto-page");
            if(page!=null && page.PageNumber==linkParameters.Parameters.PageNumber && page.PageSize == linkParameters.Parameters.PageSize)
            {
                var cachedData = _cache.GetData<List<SuperLoto>>("superloto-entities");
                if(cachedData != null && cachedData.Count() > 0)
                {
                    var cachedDtos = _mapper.Map<IEnumerable<SuperLotoDto>>(cachedData);
                    var cachedLinks = _links.TryGenerateLinks(cachedDtos, linkParameters.Parameters.Fields, linkParameters.HttpContext);
                    PagedList<SuperLoto> pagedList = new PagedList<SuperLoto>(cachedData, cachedData.Count(), linkParameters.Parameters.PageNumber, linkParameters.Parameters.PageSize);
                    return (linkResponse: cachedLinks, metaData: pagedList.MetaData);
                }
            }
            var entitiesWithMetaData = await _manager.SuperLoto.GetAllNumbersArrayAsync(linkParameters.Parameters, trackChanges);
            var sLDto = _mapper.Map<IEnumerable<SuperLotoDto>>(entitiesWithMetaData);
            var links = _links.TryGenerateLinks(sLDto, linkParameters.Parameters.Fields, linkParameters.HttpContext);

            LinkParametersDtoForCache linkParametersDtoForCache = new LinkParametersDtoForCache()
            {
                PageSize = linkParameters.Parameters.PageSize,
                PageNumber = linkParameters.Parameters.PageNumber
            };

            SetCache<PagedList<SuperLoto>>("superloto-entities", entitiesWithMetaData);
            SetCache<LinkParametersDtoForCache>("superloto-page", linkParametersDtoForCache);

            return (linkResponse:links, metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<SuperLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var cachedData = _cache.GetData<SuperLoto>($"superloto-entity-{id}");
            if(cachedData != null)
            {
                return _mapper.Map<SuperLotoDto>(cachedData);
            }
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            SetCache<SuperLoto>($"superloto-entity-{id}", entity);
            return _mapper.Map<SuperLotoDto>(entity);
        }

        public async Task<SuperLotoDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var formatedDate = FormatDate(date);
            var cachedData = _cache.GetData<SuperLoto>($"superloto-entity-{formatedDate}");
            if(cachedData != null)
            {
                return _mapper.Map<SuperLotoDto>(cachedData);
            }
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var entityDate = entities.Where(e => e.Date == date).FirstOrDefault();
            if (entityDate == null)
            {
                throw new SuperLotoDateNotFoundException(Convert.ToDateTime(date));
            }
            var reMappedEntity = _mapper.Map<SuperLoto>(entityDate);
            SetCache<SuperLoto>($"superloto-entity-{formatedDate}", reMappedEntity);
            return entityDate;
        }

        public async Task UpdateOneNumbersArrayAsync(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<SuperLoto>(superLotoDto);
            _manager.SuperLoto.UpdateOneNubersArray(entity);
            SetCache<SuperLoto>($"superloto-entity-{id}", entity);
            await _manager.SaveAsync();
        }

        public async Task<SuperLotoDtoForRandom> GetRondomNumbersAsync(string userName)
        {
            var user = await GetUser(userName);
            List<int> randomNumbers = new List<int>();
            int i = 0;
            do
            {
                var numbers = await GenerateRandomNumbersAsync();
                if (AreTheNumbersTheSame(numbers)==true)
                {
                    i++;
                    randomNumbers=numbers;
                }
            } while (i == 0);
            randomNumbers=Sort(randomNumbers);

            var matchRate = await MatchRate(randomNumbers);

            var superLotoDto = new SuperLotoDtoForRandom()
            {
                Numbers = randomNumbers,
                MatchRate = matchRate.MatchRate,
                Date = matchRate.Date
            };

            var superLotoLogs = new SuperLotoLogs()
            {
                UserName = userName,
                RandomNumbers = randomNumbers
            };

            _manager.SuperLotoLogs.CreateLog(superLotoLogs);
            await _manager.SaveAsync();

            _logger.LogInfo($"User : {user}, Random Numbers : {string.Join(",", randomNumbers)}");
            return superLotoDto;
        }

        public async Task<MatchRateDto> CompareSuperLotoNumbersAsync(SuperLotoDtoForCompare superLotoDtoForCompare)
        {
            var matchRate = await MatchRate(superLotoDtoForCompare.Numbers);
            return matchRate;
        }

        public async Task<MatchRateDto> CompareSuperLotoNumbersWithSuperLotoLogsNumbersAsync(SuperLotoDtoForCompareWithLogs superLotoDtoForCompareWithLogs)
        {
            int count = 0;
            int limit = 0;
            double calculatedMatchRate = 0;
            string date = "";
            string matchRate = "";
            var logs = await _manager.SuperLotoLogs.GetAllLogsAsync(false);
            foreach (var log in logs)
            {
                var logNumbers = log.RandomNumbers;
                for(int i = 0; i<superLotoDtoForCompareWithLogs.Numbers.Count(); i++)
                {
                    for(int j = 0; j<logNumbers.Count(); j++)
                    {
                        if (superLotoDtoForCompareWithLogs.Numbers[i] == logNumbers[j])
                        {
                            count++;
                        }
                        if(count > limit)
                        {
                            calculatedMatchRate = CalculateMatchRate(count);
                            matchRate = calculatedMatchRate.ToString();
                            limit = count;
                            date = log.Date.ToString();
                        }
                    }
                }
                count = 0;
            }
            if(string.IsNullOrEmpty(calculatedMatchRate.ToString()))
            {
                matchRate = "No matching";
            }
            var matchRateDto = new MatchRateDto()
            {
                MatchRate = matchRate,
                Date = date
            };
            return matchRateDto;
        }

        private async Task<MatchRateDto> MatchRate(List<int> randomNumbers)
        {
            int count = 0;
            int limit = 0;
            double calculatedMatchRate = 0;
            string date = "";
            string matchRate = "";
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(false);
            foreach (var entity in entities)
            {
                var entityNumbers = entity.Numbers;
                for (int i = 0; i < randomNumbers.Count(); i++)
                {
                    for (int j = 0; j < entityNumbers.Count(); j++)
                    {
                        if (randomNumbers[j] == entityNumbers[i])
                        {
                            count++;
                        }
                        if (count > limit)
                        {
                            calculatedMatchRate = CalculateMatchRate(count);
                            matchRate = calculatedMatchRate.ToString();
                            limit = count;
                            date = entity.Date.ToString();
                        }
                    }
                }
                count = 0;
            }
            if (string.IsNullOrEmpty(calculatedMatchRate.ToString()))
            {
                matchRate = "No Matching";
            }
            var matchRateDto = new MatchRateDto()
            {
                MatchRate = matchRate,
                Date = date
            };
            return matchRateDto;
        }

        private double CalculateMatchRate(int count)
        {
            var matchRate = ((double)count / 6) * 100;
            matchRate = Math.Round(matchRate, 2);
            return matchRate;
        }

        private async Task<string> GetUser(string userName)
        {
            User user = new User();
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
                return user.UserName.ToString();
            }
            user.UserName = GenerateRandomUserName();
            return "Guest-" + user.UserName.ToString();
        }

        private string? GenerateRandomUserName()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] randomUserName = new char[16];
            for (int i = 0; i < randomUserName.Length; i++)
            {
                randomUserName[i] = characters[random.Next(characters.Length)];
            }
            return new string(randomUserName);
        }

        private async Task<List<int>> GenerateRandomNumbersAsync()
        {
            int index;
            int selectedNumber;
            int sleepTimeInSeconds = 1;
            var numbers = await GetOnlyNumbersAsync(false);
            int totalCount = numbers.Count();
            long ticks = DateTime.Now.Ticks;
            Random random = new Random((int)ticks);
            var randomNumbers = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                Thread.Sleep(sleepTimeInSeconds);
                index = random.Next(0, totalCount-1);
                selectedNumber = numbers.ElementAt(index);
                randomNumbers.Add(selectedNumber);
            }
            return randomNumbers.ToList();
        }

        private bool AreTheNumbersTheSame(List<int> numbers)
        {
            if (numbers.Count != 6)
            {
                return false;
            }

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] == numbers[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private List<int> Sort(List<int> numbers)
        {
            List<int> sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();
            return sortedNumbers;
        }
        
        private async Task<SuperLoto> GetOneNumbersArrayByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.SuperLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if(entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }
            return entity;
        }

        private async Task<IEnumerable<int>> GetOnlyNumbersAsync(bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var numbers = entities.SelectMany(e => e.Numbers).ToList();
            return numbers;
        }

        private async Task<IEnumerable<SuperLotoDto>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges)
        {
            var entities = await _manager.SuperLoto.GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            return _mapper.Map<IEnumerable<SuperLotoDto>>(entities);
        }

        private void SetCache<T>(string key, T value)
        {
            var expiryTime = DateTimeOffset.Now.AddSeconds(120);
            _cache.SetData(key, value, expiryTime);
        }

        private string FormatDate(DateTime date)
        {
            var day = date.Day;
            var month = date.Month;
            var year = date.Year;
            return $"{day}/{month}/{year}";
        }
    }
}
