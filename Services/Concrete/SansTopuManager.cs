using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Cantracts;
using Services.Contracts;
using System.Security.Claims;

namespace Services.Concrete
{
    public class SansTopuManager : ISansTopuService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly ISansTopuLinks _sansTopuLinks;
        private readonly UserManager<User> _userManager;

        public SansTopuManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, ISansTopuLinks sansTopuLinks, UserManager<User> userManager)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _sansTopuLinks = sansTopuLinks;
            _userManager = userManager;
        }

        public async Task<SansTopuDto> CreateOneNumbersArrayAsync(SansTopuDtoForInsertion sansTopuDtoForInsertion)
        {
            var entity = _mapper.Map<SansTopu>(sansTopuDtoForInsertion);
            _manager.SansTopu.CreateOneNumbersArray(entity);
            await _manager.SaveAsync();
            return _mapper.Map<SansTopuDto>(entity);
        }

        public async Task DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            _manager.SansTopu.DeleteOneNumbersArray(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData )> GetAllNumbersArraysAsync(LinkParameters<SansTopuParameters> linkParameters, bool trackChanges)
        {
            var entitiesWithMetaData = await _manager.SansTopu.GetAllNumbersArrayAsync(linkParameters.Parameters, trackChanges);
            var stDto = _mapper.Map<IEnumerable<SansTopuDto>>(entitiesWithMetaData);
            var links = _sansTopuLinks.TryGenerateLinks(stDto, linkParameters.Parameters.Fields, linkParameters.HttpContext);
            return (linkResponse: links, metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<SansTopuDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var entityDate = entities.Where(e => e.Date == date).FirstOrDefault();
            if (entityDate == null)
            {
                throw new SansTopuDateNotFoundException(Convert.ToDateTime(date));
            }
            return entityDate;
        }

        public async Task<SansTopuDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<SansTopuDto>(entity);
        }

        public async Task UpdateOneNumbersArrayAsync(int id, SansTopuDtoForUpdate sansTopuDtoForUpdate, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<SansTopu>(sansTopuDtoForUpdate);
            _manager.SansTopu.Update(entity);
            await _manager.SaveAsync();
        }

        public async Task<SansTopuDtoForRandom> GetRondomNumbersAsync(HttpContext context)
        {
            var user = await GetUser(context);
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

            var sansTopuDto = new SansTopuDtoForRandom()
            {
                PlusNumber = randomPlusNumber,
                Numbers = randomNumbers
            };

            _logger.LogInfo($"User :{user.UserName}, Random PlusNumber : {randomPlusNumber}, Random Numbers : {string.Join(",", randomNumbers)}");

            return sansTopuDto; 
        }

        private async Task<UserDtoForGetRandomNumbers> GetUser(HttpContext context)
        {
            var userName = context.User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var userDto = new UserDtoForGetRandomNumbers()
            {
                UserId = user.Id,
                UserName = user.UserName
            };
            return userDto;
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
    }
}
