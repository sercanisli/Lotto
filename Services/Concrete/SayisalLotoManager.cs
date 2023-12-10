using AutoMapper;
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
    public class SayisalLotoManager : ISayisalLotoService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly ISayisalLotoLinks _links;
        private readonly UserManager<User> _userManager;

        public SayisalLotoManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, ISayisalLotoLinks links, UserManager<User> userManager)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _links = links;
            _userManager = userManager;
        }

        public async Task<SayisalLotoDto> CreateOneNumbersArrayAsync(SayisalLotoDtoForInsertion sayisalLotoDtoForInsertion)
        {
            var entity = _mapper.Map<SayisalLoto>(sayisalLotoDtoForInsertion);
            _manager.SayisalLoto.CreateOneNumbersArray(entity);
            await _manager.SaveAsync();
            return _mapper.Map<SayisalLotoDto>(entity);
        }

        public async Task DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            _manager.SayisalLoto.DeleteOneNumbersArray(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<SayisalLotoParameters> linkParameters, bool trackChanges)
        {
            var entitiesWithMetaData = await _manager.SayisalLoto.GetAllNumbersArrayAsync(linkParameters.Parameters, trackChanges);
            var sayisalLotosDto = _mapper.Map<IEnumerable<SayisalLotoDto>>(entitiesWithMetaData);
            var links = _links.TryGenerateLinks(sayisalLotosDto, linkParameters.Parameters.Fields, linkParameters.HttpContext);
            return (linkResponse:links , metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<SayisalLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<SayisalLotoDto>(entity);
        }

        public async Task<SayisalLotoDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var entityDate = entities.Where(e=>e.Date == date).FirstOrDefault();
            if(entityDate == null)
            {
                throw new SayisalLotoDateNotFoundException(Convert.ToDateTime(date));
            }
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

            var sayisalLotoDto = new SayisalLotoDtoForRandom()
            {
                Numbers = randomNumbers
            };
            _logger.LogInfo($"User : {user}, Random Numbers : {string.Join(",", randomNumbers)}");
            return sayisalLotoDto;
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
    }
}
