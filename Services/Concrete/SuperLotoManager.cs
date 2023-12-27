using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Repositories.Cantracts;
using Services.Contracts;
using System.Dynamic;

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
            return _mapper.Map<SuperLotoDto>(entity);
        }

        public async Task DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            _manager.SuperLoto.DeleteOneNumbersArray(entity);
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

            

            return (linkResponse:links, metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<SuperLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<SuperLotoDto>(entity);
        }

        public async Task<SuperLotoDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var entityDate = entities.Where(e => e.Date == date).FirstOrDefault();
            if (entityDate == null)
            {
                throw new SuperLotoDateNotFoundException(Convert.ToDateTime(date));
            }
            return entityDate;
        }

        public async Task UpdateOneNumbersArrayAsync(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);

            entity = _mapper.Map<SuperLoto>(superLotoDto);
            _manager.SuperLoto.UpdateOneNubersArray(entity);
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
            var superLotoDto = new SuperLotoDtoForRandom()
            {
                Numbers = randomNumbers,
            };

            _logger.LogInfo($"User : {user}, Random Numbers : {string.Join(",", randomNumbers)}");
            return superLotoDto;
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
    }
}
