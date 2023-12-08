﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class OnNumaraManager : IOnNumaraService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IOnNumaraLinks _onNumaraLinks;
        private readonly UserManager<User> _userManager;
        public OnNumaraManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IOnNumaraLinks onNumaraLinks, UserManager<User> userManager)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _onNumaraLinks = onNumaraLinks;
            _userManager = userManager;
        }

        public async Task<OnNumaraDto> CreateOneNumbersArrayAsync(OnNumaraDtoForInsertion onNumaraDtoForInsertion)
        {
            var entity = _mapper.Map<OnNumara>(onNumaraDtoForInsertion);
            _manager.OnNumara.CreateOneNumbersArray(entity);
            await _manager.SaveAsync();
            return _mapper.Map<OnNumaraDto>(entity);
        }

        public async Task DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            _manager.OnNumara.DeleteOneNumbersArray(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<OnNumaraParameters> linkParameters, bool trackChanges)
        {
            var entitiesWithMetaData = await _manager.OnNumara.GetAllNumbersArrayAsync(linkParameters.Parameters ,trackChanges);
            var dtos = _mapper.Map<IEnumerable<OnNumaraDto>>(entitiesWithMetaData);
            var links = _onNumaraLinks.TryGenerateLinks(dtos, linkParameters.Parameters.Fields, linkParameters.HttpContext);
            return (linkResponse:links, metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<OnNumaraDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<OnNumaraDto>(entity);
        }

        public async Task<OnNumaraDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var entityDate = entities.Where(e => e.Date == date).FirstOrDefault();
            if (entityDate == null)
            {
                throw new OnNumaraDateNotFoundException(Convert.ToDateTime(date));
            }
            return entityDate;
        }

        public async Task UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges)
        {
            if(onNumaraDtoForUpdate == null)
            {
                throw new ArgumentNullException(nameof(onNumaraDtoForUpdate));
            }
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<OnNumara>(onNumaraDtoForUpdate);
            _manager.OnNumara.UpdateOneNumbersArray(entity);
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

            var onNumaraDto = new OnNumaraDtoForRandom()
            {
                Numbers = randomNumbers,
            };
            return onNumaraDto;
        }

        private async Task<string> GetUser(string userName)
        {
            User user = new User();
            if(userName == null)
            {
                user.UserName = GenerateRandomUserName();
            }
            else
            {
                user = await _userManager.FindByNameAsync(userName);
            }
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

        
    }
}
