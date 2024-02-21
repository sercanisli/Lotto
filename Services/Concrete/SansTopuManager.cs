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
    public class SansTopuManager : ISansTopuService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly ISansTopuLinks _sansTopuLinks;
        private readonly UserManager<User> _userManager;
        private readonly ICacheService _cache;
        public SansTopuManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, ISansTopuLinks sansTopuLinks, UserManager<User> userManager, ICacheService cache)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _sansTopuLinks = sansTopuLinks;
            _userManager = userManager;
            _cache = cache;
        }

        public async Task<SansTopuDto> CreateOneNumbersArrayAsync(SansTopuDtoForInsertion sansTopuDtoForInsertion)
        {
            var entity = _mapper.Map<SansTopu>(sansTopuDtoForInsertion);
            _manager.SansTopu.CreateOneNumbersArray(entity);
            await _manager.SaveAsync();
            SetCache<SansTopu>($"sanstopu-entity-{entity.Id}", entity);
            return _mapper.Map<SansTopuDto>(entity);
        }

        public async Task DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            _manager.SansTopu.DeleteOneNumbersArray(entity);
            _cache.RemoveData($"sanstopu-entity-{id}");
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData )> GetAllNumbersArraysAsync(LinkParameters<SansTopuParameters> linkParameters, bool trackChanges)
        {
            var page = _cache.GetData<LinkParametersDtoForCache>("sanstopu-page");
            if(page != null && page.PageNumber == linkParameters.Parameters.PageNumber && page.PageSize == linkParameters.Parameters.PageSize)
            {
                var cachedData = _cache.GetData<List<SansTopu>>("sanstopu-entities");
                if(cachedData != null && cachedData.Count() > 0)
                {
                    var cachedDtos = _mapper.Map<IEnumerable<SansTopuDto>>(cachedData);
                    var cachedLinks = _sansTopuLinks.TryGenerateLinks(cachedDtos, linkParameters.Parameters.Fields, linkParameters.HttpContext);
                    PagedList<SansTopu> pagedList = new PagedList<SansTopu>(cachedData, cachedData.Count(), linkParameters.Parameters.PageNumber, linkParameters.Parameters.PageSize);
                    return (linkResponse: cachedLinks, metaData: pagedList.MetaData);
                }
            }
            var entitiesWithMetaData = await _manager.SansTopu.GetAllNumbersArrayAsync(linkParameters.Parameters, trackChanges);
            var stDto = _mapper.Map<IEnumerable<SansTopuDto>>(entitiesWithMetaData);
            var links = _sansTopuLinks.TryGenerateLinks(stDto, linkParameters.Parameters.Fields, linkParameters.HttpContext);

            LinkParametersDtoForCache linkParametersDtoForCache = new LinkParametersDtoForCache()
            {
                PageNumber = linkParameters.Parameters.PageNumber,
                PageSize = linkParameters.Parameters.PageSize
            };

            SetCache<PagedList<SansTopu>>("sanstopu-entities", entitiesWithMetaData);
            SetCache<LinkParametersDtoForCache>("sanstopu-page", linkParametersDtoForCache);

            return (linkResponse: links, metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<SansTopuDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var formatedDate = FormatDate(date);
            var cachedData = _cache.GetData<SansTopu>($"sanstopu-entity-{formatedDate}");
            if(cachedData!=null)
            {
                return _mapper.Map<SansTopuDto>(cachedData);
            }
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var entityDate = entities.Where(e => e.Date == date).FirstOrDefault();
            if (entityDate == null)
            {
                throw new SansTopuDateNotFoundException(Convert.ToDateTime(date));
            }
            var reMappedEntity = _mapper.Map<SansTopu>(entityDate);
            SetCache<SansTopu>($"sanstopu-entity-{formatedDate}", reMappedEntity);
            return entityDate;
        }

        public async Task<SansTopuDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var cachedData = _cache.GetData<SansTopu>($"sanstopu-entity-{id}");
            if (cachedData != null)
            {
                return _mapper.Map<SansTopuDto>(cachedData);
            }
            cachedData = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);

            if(cachedData == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }

            SetCache<SansTopu>($"sanstopu-entity-{id}", cachedData);

            return _mapper.Map<SansTopuDto>(cachedData);
        }

        public async Task UpdateOneNumbersArrayAsync(int id, SansTopuDtoForUpdate sansTopuDtoForUpdate, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<SansTopu>(sansTopuDtoForUpdate);
            _manager.SansTopu.UpdateOneNumbersArray(entity);
            SetCache<SansTopu>($"sanstopu-entity-{id}", entity);
            await _manager.SaveAsync();
        }

        public async Task<SansTopuDtoForRandom> GetRondomNumbersAsync(string userName)
        {
            var user = await GetUser(userName);
            var randomPlusNumber = await GenerateRandomPlusNumberAsync();
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

            var matchRate = await MatchRate(randomNumbers, randomPlusNumber);

            var sansTopuDto = new SansTopuDtoForRandom()
            {
                MatchRate = matchRate.MatchRate,
                Date = matchRate.Date,
                PlusNumber = randomPlusNumber,
                Numbers = randomNumbers
            };
            var sansTopuLogs = new SansTopuLogs()
            {
                UserName = user,
                RandomPlusNumber = randomPlusNumber,
                RandomNumbers = randomNumbers
            };
             _manager.SansTopuLogs.CreateLog(sansTopuLogs);
            await _manager.SaveAsync();
            _logger.LogInfo($"User :{user}, Random PlusNumber : {randomPlusNumber}, Random Numbers : {string.Join(",", randomNumbers)}");

            return sansTopuDto; 
        }

        public async Task<MatchRateDto> CompareSansTopuNumbersAsync(SansTopuDtoForCompare sansTopuDtoForCompare)
        {
            var matchRate = await MatchRate(sansTopuDtoForCompare.Numbers, sansTopuDtoForCompare.PlusNumber);
            return matchRate;
        }

        public async Task<MatchRateDto> CompareSansTopuNumbersWithSansTopuLogsNumbersAsync(SansTopuDtoForCompareWithLogs sansTopuDtoForCompareWithLogs)
        {
            int count = 0;
            int limit = 0;
            double calculatedMatchRate = 0;
            string date = "";
            string matchRate = "";
            var logs = await _manager.SansTopuLogs.GetAllLogsAsync(false);
            foreach(var log in logs)
            {
                var logNumbers = log.RandomNumbers;
                int logPlusNumber = log.RandomPlusNumber;
                if(logPlusNumber==sansTopuDtoForCompareWithLogs.PlusNumber)
                {
                    count++;
                    limit = count;
                }
                for(int i = 0; i<sansTopuDtoForCompareWithLogs.Numbers.Count(); i++)
                {
                    for(int j = 0; j<logNumbers.Count(); j++)
                    {
                        if (sansTopuDtoForCompareWithLogs.Numbers[i] == logNumbers[j])
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
            if (string.IsNullOrEmpty(calculatedMatchRate.ToString()))
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

        private async Task<MatchRateDto> MatchRate(List<int> randomNumbers, int randomPlusNumber)
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
                var entityPlusNumber = entity.PlusNumber;
                if (randomPlusNumber == entityPlusNumber)
                {
                    count++;
                    limit = count;
                }
                for(int i = 0; i<randomNumbers.Count(); i++)
                {
                    for(int j = 0; j<entityNumbers.Count(); j++)
                    {
                        if (randomNumbers[j] == entityNumbers[i])
                        {
                            count++;
                        }
                        if(count>limit)
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
            if(string.IsNullOrEmpty(calculatedMatchRate.ToString()))
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

        private string? GenerateRandomUserName()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] randomUserName = new char[16];
            for(int i = 0; i< randomUserName.Length; i++)
            {
                randomUserName[i] = characters[random.Next(characters.Length)];
            }
            return new string(randomUserName);
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

        private List<int> Sort(List<int> randomNumbers)
        {
            List<int> sortedNumbers = randomNumbers.ToList();
            sortedNumbers.Sort();
            return sortedNumbers;
        }

        private bool AreTheNumbersTheSame(List<int> numbers)
        {
            if (numbers.Count != 5)
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

        private async Task<int> GenerateRandomPlusNumberAsync()
        {
            var plusNumbers = await GetOnlyPlusNumbersAsync(false);
            var randomPlusNumber = Random(plusNumbers);
            return randomPlusNumber;
        }

        private async Task<List<int>> GetOnlyPlusNumbersAsync(bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var plusNumbers = entities.Select(e => e.PlusNumber).ToList();
            return plusNumbers;
        }

        private async Task<List<int>> GenerateRandomNumbersAsync()
        {
            var randomNumbers = new List<int>();
            var numbers = await GetOnlyNumbersAsync(false);
            for (int i=0; i < 5; i++)
            {
                var randomNumber = Random(numbers);
                randomNumbers.Add(randomNumber);
            }
            return randomNumbers.ToList();
        }

        private async Task<List<int>> GetOnlyNumbersAsync(bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var numbers = entities.SelectMany(e => e.Numbers).ToList();
            return numbers;
        }

        private int Random(List<int> numbers)
        {
            int index;
            int randomNumber;
            int sleepTimeInSeconds = 1;
            int totalCount = numbers.Count();
            long ticks = DateTime.Now.Ticks;
            Random random = new Random((int)ticks);
            Thread.Sleep(sleepTimeInSeconds);
            index = random.Next(0, totalCount - 1);
            randomNumber= numbers.ElementAt(index);
            return randomNumber;
        }

        private async Task<SansTopu> GetOneNumbersArrayByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.SansTopu.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            return entity;
        }

        private async Task<IEnumerable<SansTopuDto>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges)
        {
            var entities = await _manager.SansTopu.GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            return _mapper.Map<IEnumerable<SansTopuDto>>(entities);
        }

        private void SetCache<T>(string key, T value)
        {
            var expiryTime = DateTimeOffset.Now.AddSeconds(120);
            _cache.SetData(key , value, expiryTime);
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
