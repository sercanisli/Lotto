﻿using AutoMapper;
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
    public class OnNumaraManager : IOnNumaraService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IOnNumaraLinks _onNumaraLinks;
        private readonly UserManager<User> _userManager;
        private readonly ICacheService _cache;

        public OnNumaraManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IOnNumaraLinks onNumaraLinks, UserManager<User> userManager, ICacheService cache)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _onNumaraLinks = onNumaraLinks;
            _userManager = userManager;
            _cache = cache;
        }

        public async Task<OnNumaraDto> CreateOneNumbersArrayAsync(OnNumaraDtoForInsertion onNumaraDtoForInsertion)
        {
            var entity = _mapper.Map<OnNumara>(onNumaraDtoForInsertion);
            _manager.OnNumara.CreateOneNumbersArray(entity);
            await _manager.SaveAsync();
            SetCache<OnNumara>($"onnumara-entity-{entity.Id}", entity);
            return _mapper.Map<OnNumaraDto>(entity);
        }

        public async Task DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            _manager.OnNumara.DeleteOneNumbersArray(entity);
            _cache.RemoveData($"onnumara-entity-{id}");
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<OnNumaraParameters> linkParameters, bool trackChanges)
        {
            var page = _cache.GetData<LinkParametersDtoForCache>("onnumara-page");
            if (page != null && page.PageNumber == linkParameters.Parameters.PageNumber && page.PageSize == linkParameters.Parameters.PageSize)
            {
                var cachedData = _cache.GetData<List<OnNumara>>("onnumara-entities");
                var metadData = _cache.GetData<MetaData>("onnumara-metaData");
                if (cachedData != null && cachedData.Count() > 0)
                {
                    var cachedDtos = _mapper.Map<IEnumerable<OnNumaraDto>>(cachedData);
                    var cachedLinks = _onNumaraLinks.TryGenerateLinks(cachedDtos, linkParameters.Parameters.Fields, linkParameters.HttpContext);
                    PagedList<OnNumara> pagedList = new PagedList<OnNumara>(cachedData, metadData.TotalCount, linkParameters.Parameters.PageNumber, linkParameters.Parameters.PageSize);
                    return (linkResponse: cachedLinks, metaData: pagedList.MetaData);
                }
            }

            var entitiesWithMetaData = await _manager.OnNumara.GetAllNumbersArrayAsync(linkParameters.Parameters, trackChanges);
            var metaData = entitiesWithMetaData.MetaData;
            var dtos = _mapper.Map<IEnumerable<OnNumaraDto>>(entitiesWithMetaData);
            var links = _onNumaraLinks.TryGenerateLinks(dtos, linkParameters.Parameters.Fields, linkParameters.HttpContext);

            LinkParametersDtoForCache linkParametersDtoForCache = new LinkParametersDtoForCache()
            {
                PageSize = linkParameters.Parameters.PageSize,
                PageNumber = linkParameters.Parameters.PageNumber
            };

            SetCache<PagedList<OnNumara>>("onnumara-entities", entitiesWithMetaData);
            SetCache<LinkParametersDtoForCache>("onnumara-page", linkParametersDtoForCache);
            SetCache<MetaData>("onnumara-metaData", metaData);

            return (linkResponse: links, metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<OnNumaraDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken = default)
        {
            var cachedData = _cache.GetData<OnNumara>($"onnumara-entity-{id}");
            if (cachedData != null)
            {
                var cache = _mapper.Map<OnNumaraDto>(cachedData);
                return cache;
            }
            cachedData = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);

            if(cachedData == null)
            {
                throw new OnNumaraNotFoundException(id);
            }

            SetCache<OnNumara>($"onnumara-entity-{id}", cachedData);

            return _mapper.Map<OnNumaraDto>(cachedData);
        }

        public async Task<OnNumaraDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var formatedDate = FormatDate(date);
            var cachedData = _cache.GetData<OnNumara>($"onnumara-entity-{formatedDate}");
            if (cachedData != null)
            {
                return _mapper.Map<OnNumaraDto>(cachedData);
            }
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var entityDate = entities.Where(e => e.Date == date).FirstOrDefault();
            if (entityDate == null)
            {
                throw new OnNumaraDateNotFoundException(Convert.ToDateTime(date));
            }
            var reMappedEntity = _mapper.Map<OnNumara>(entityDate);
            SetCache<OnNumara>($"onnumara-entity-{formatedDate}", reMappedEntity);
            return entityDate;
        }

        public async Task UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges)
        {
            if (onNumaraDtoForUpdate == null)
            {
                throw new ArgumentNullException(nameof(onNumaraDtoForUpdate));
            }
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<OnNumara>(onNumaraDtoForUpdate);
            _manager.OnNumara.UpdateOneNumbersArray(entity);
            SetCache<OnNumara>($"onnumara-entity-{id}", entity);
            await _manager.SaveAsync();
        }

        public async Task<OnNumaraDtoForRandom> GetRondomNumbersAsync(string userName)
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

            var onNumaraDto = new OnNumaraDtoForRandom()
            {
                MatchRate = matchRate.MatchRate,
                Date = matchRate.Date,
                Numbers = randomNumbers
            };

            var onNumaraLogs = new OnNumaraLogs()
            {
                UserName = user,
                RandomNumbers = randomNumbers
            };
            _manager.OnNumaraLogs.CreateLog(onNumaraLogs);
            await _manager.SaveAsync();
            _logger.LogInfo($"User :{user}, Random Numbers : {string.Join(",", randomNumbers)}");
            return onNumaraDto;
        }

        public async Task<MatchRateDto> CompareOnNumaraNumbersAsync(OnNumaraDtoForCompare onNumaraDtoForCompare)
        {
            var matchRate = await MatchRate(onNumaraDtoForCompare.Numbers);
            return matchRate;
        }

        public async Task<MatchRateDto> CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync(OnNumaraDtoForCompareWithLogs onNumaraDtoForCompareWithLogs)
        {
            int count = 0;
            int limit = 0;
            double calculatedMatchRate = 0;
            string date = "";
            string matchRate = "";
            var logs = await _manager.OnNumaraLogs.GetAllLogsAsync(false);
            foreach (var log in logs)
            {
                var entityNumbers = log.RandomNumbers;
                for (int i = 0; i < onNumaraDtoForCompareWithLogs.Numbers.Count(); i++)
                {
                    for (int j = 0; j < entityNumbers.Count(); j++)
                    {
                        if (onNumaraDtoForCompareWithLogs.Numbers[i] == entityNumbers[j])
                        {
                            count++;
                        }
                        if (count > limit)
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
                matchRate = "No Matching";
            }
            var matchRateDto = new MatchRateDto()
            {
                MatchRate = matchRate,
                Date = date
            };
            return matchRateDto;
        }

        public async Task<OnNumaraDtoForLastItem> GetLastItemAsync(bool trackChanges)
        {
            var arrays = await GetAllNumbersArrayWithoutPaginationAsync(false);
            var lastArray = arrays.LastOrDefault();
            var returnedLastArray = _mapper.Map<OnNumaraDtoForLastItem>(lastArray);
            return returnedLastArray;
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
                        if (randomNumbers[i] == entityNumbers[j])
                        {
                            count++;
                        }
                        if (count >= limit)
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
            var matchRate = ((double)count / 10) * 100;
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

        private List<int> Sort(List<int> numbers)
        {
            List<int> sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();
            return sortedNumbers;
        }

        private bool AreTheNumbersTheSame(List<int> numbers)
        {
            if (numbers.Count != 10)
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

            for (int i = 0; i < 10; i++)
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

        private async Task<IEnumerable<OnNumaraDto>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges)
        {
            var entities = await _manager.OnNumara.GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            return _mapper.Map<IEnumerable<OnNumaraDto>>(entities);
        }

        private async Task<OnNumara> GetOneNumbersArrayByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.OnNumara.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new OnNumaraNotFoundException(id);
            }
            return entity;
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
