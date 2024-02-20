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
using System.Globalization;

namespace Services.Concrete
{
    public class SayisalLotoManager : ISayisalLotoService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly ISayisalLotoLinks _links;
        private readonly UserManager<User> _userManager;
        private readonly ICacheService _cache;

        public SayisalLotoManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, ISayisalLotoLinks links, UserManager<User> userManager, ICacheService cache)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _links = links;
            _userManager = userManager;
            _cache = cache;
        }

        public async Task<SayisalLotoDto> CreateOneNumbersArrayAsync(SayisalLotoDtoForInsertion sayisalLotoDtoForInsertion)
        {
            var entity = _mapper.Map<SayisalLoto>(sayisalLotoDtoForInsertion);
            _manager.SayisalLoto.CreateOneNumbersArray(entity);
            await _manager.SaveAsync();
            SetCache<SayisalLoto>($"sayisalloto-entity-{entity.Id}", entity);
            return _mapper.Map<SayisalLotoDto>(entity);
        }

        public async Task DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            _manager.SayisalLoto.DeleteOneNumbersArray(entity);
            _cache.RemoveData($"sayisalloto-entity-{id}");
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<SayisalLotoParameters> linkParameters, bool trackChanges)
        {
            var page = _cache.GetData<LinkParametersDtoForCache>("sayisalloto-page");
            if(page != null && page.PageNumber == linkParameters.Parameters.PageNumber && page.PageSize == linkParameters.Parameters.PageSize)
            {
                var cachedData = _cache.GetData<List<SayisalLoto>>("sayisalloto-entities");
                var metadData = _cache.GetData<MetaData>("sayisal-loto-metaData");
                if(cachedData != null && cachedData.Count() > 0)
                {
                    var cachedDtos = _mapper.Map<IEnumerable<SayisalLotoDto>>(cachedData);
                    var cachedLinks = _links.TryGenerateLinks(cachedDtos, linkParameters.Parameters.Fields, linkParameters.HttpContext);
                    PagedList<SayisalLoto> pagedList = new PagedList<SayisalLoto>(cachedData, metadData.TotalCount, linkParameters.Parameters.PageNumber, linkParameters.Parameters.PageSize);
                    return (linkResponse: cachedLinks, metaData: pagedList.MetaData);
                }
            }
            var entitiesWithMetaData = await _manager.SayisalLoto.GetAllNumbersArrayAsync(linkParameters.Parameters, trackChanges);
            var metaData = entitiesWithMetaData.MetaData;
            var sayisalLotosDto = _mapper.Map<IEnumerable<SayisalLotoDto>>(entitiesWithMetaData);
            var links = _links.TryGenerateLinks(sayisalLotosDto, linkParameters.Parameters.Fields, linkParameters.HttpContext);

            LinkParametersDtoForCache linkParametersDtoForCache = new LinkParametersDtoForCache()
            {
                PageSize = linkParameters.Parameters.PageSize,
                PageNumber = linkParameters.Parameters.PageNumber
            };

            SetCache<PagedList<SayisalLoto>>("sayisalloto-entities", entitiesWithMetaData);
            SetCache<LinkParametersDtoForCache>("sayisalloto-page", linkParametersDtoForCache);
            SetCache<MetaData>("sayisal-loto-metaData", metaData);

            return (linkResponse:links , metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<SayisalLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var cachedData = _cache.GetData<SayisalLoto>($"sayisalloto-entity-{id}");
            if(cachedData != null)
            {
                return _mapper.Map<SayisalLotoDto>(cachedData);
            }
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);

            if(entity == null)
            {
                throw new SayisalLotoNotFoundException(id);
            }

            SetCache<SayisalLoto>($"sayisalloto-entity-{id}", entity);

            return _mapper.Map<SayisalLotoDto>(entity);
        }

        public async Task<SayisalLotoDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var formatedDate = FormatDate(date);
            var cachedData = _cache.GetData<SayisalLoto>($"sayisalloto-entity-{formatedDate}");
            if(cachedData != null)
            {
                return _mapper.Map<SayisalLotoDto>(cachedData);
            }
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var entityDate = entities.Where(e=>e.Date == date).FirstOrDefault();
            if(entityDate == null)
            {
                throw new SayisalLotoDateNotFoundException(Convert.ToDateTime(date));
            }
            var reMappedEntity = _mapper.Map<SayisalLoto>(entityDate);
            SetCache<SayisalLoto>($"sayisalloto-entity-{formatedDate}", reMappedEntity);
            return entityDate;
        }

        public async Task UpdateOneNumbersArrayAsync(int id, SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<SayisalLoto>(sayisalLotoDtoForUpdate);
            _manager.SayisalLoto.UpdateOneNumbersArray(entity);
            await _manager.SaveAsync();
        }

        public async Task<SayisalLotoDtoForRandom> GetRondomNumbersAsync(string userName)
        {
            var user = await GetUser(userName);
            List<int> randomNumbers = new List<int>();
            int i = 0;
            do
            {
                var numbers = await GenerateRandomNumbersAsync();
                if (AreTheNumbersTheSame(numbers) == true)
                {
                    i++;
                    randomNumbers = numbers;
                }
            } while (i == 0);
            randomNumbers = Sort(randomNumbers);

            var matchRate = await MatchRate(randomNumbers);

            var sayisalLotoDto = new SayisalLotoDtoForRandom()
            {
                Numbers = randomNumbers,
                MatchRate = matchRate.MatchRate,
                Date = matchRate.Date
            };

            var sayisalLotoLogs = new SayisalLotoLogs()
            {
                UserName = user,
                RandomNumbers = randomNumbers
            };

            _manager.SayisalLotoLogs.CreateLog(sayisalLotoLogs);
            await _manager.SaveAsync();
            _logger.LogInfo($"User : {user}, Random Numbers : {string.Join(",", randomNumbers)}");
            return sayisalLotoDto;
        }

        public async Task<MatchRateDto> CompareSayisalLotoNumbersAsync(SayisalLotoDtoForCompare sayisalLotoDtoForCompare)
        {
            var matchRate = await MatchRate(sayisalLotoDtoForCompare.Numbers);
            return matchRate;
        }

        public async Task<MatchRateDto> CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync(SayisalLotoDtoForCompareWithLogs sayisalLotoDtoForCompareWithLogs)
        {
            int count = 0;
            int limit = 0;
            double calculatedMatchRate = 0;
            string date = "";
            string matchRate = "";
            var entities = await _manager.SayisalLotoLogs.GetAllLogsAsync(false);
            foreach (var entity in entities)
            {
                var entityNumbers = entity.RandomNumbers;
                for(int i = 0; i<sayisalLotoDtoForCompareWithLogs.Numbers.Count(); i++)
                {
                    for(int j = 0; j<entityNumbers.Count(); j++)
                    {
                        if (sayisalLotoDtoForCompareWithLogs.Numbers[i] == entityNumbers[j])
                        {
                            count++;
                        }
                        if(count>limit)
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

        private List<int> Sort(List<int> numbers)
        {
            List<int> sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();
            return sortedNumbers;
        }

        private bool AreTheNumbersTheSame(List<int> numbers)
        {
            if(numbers.Count != 6)
            {
                return false;
            }
            for(int i = 0; i<numbers.Count -1; i++)
            {
                for(int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] == numbers[j])
                    {
                        return false;
                    }
                }
            }
            return true;
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

            for(int i = 0; i < 6; i++)
            {
                Thread.Sleep(sleepTimeInSeconds);
                index = random.Next(0, totalCount - 1);
                selectedNumber = numbers.ElementAt(index);
                randomNumbers.Add(selectedNumber);
            }
            return randomNumbers.ToList();
        }

        private async Task<IEnumerable<int>> GetOnlyNumbersAsync(bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var numbers = entities.SelectMany(e => e.Numbers).ToList();
            return numbers;
        }

        private async Task<SayisalLoto> GetOneNumbersArrayByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.SayisalLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if(entity == null)
            {
                throw new SayisalLotoNotFoundException(id);
            }
            return entity;
        }

        private async Task<IEnumerable<SayisalLotoDto>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges)
        {
            var entities = await _manager.SayisalLoto.GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            return _mapper.Map<IEnumerable<SayisalLotoDto>>(entities);
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

        private async Task<MatchRateDto> MatchRate(List<int> randomNumbers)
        {
            int count = 0;
            int limit = 0;
            double calculatedMatchRate = 0;
            string date = "";
            string matchRate = "";
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(false);
            foreach(var entity in entities)
            {
                var entityNumbers = entity.Numbers;
                for(int j = 0; j<randomNumbers.Count(); j++)
                {
                    for(int k = 0; k<entityNumbers.Count() ; k++)
                    {
                        if (randomNumbers[k] == entityNumbers[j])
                        {
                            count++;
                        }
                        if (count > limit)
                        {
                            calculatedMatchRate = CalculateMatchRate(count);
                            matchRate = calculatedMatchRate.ToString();
                            limit = count;
                            date = entity.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
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
    }
}
