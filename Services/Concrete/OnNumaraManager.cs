using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class OnNumaraManager : IOnNumaraService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public OnNumaraManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
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
            var entity = await _manager.OnNumara.GetOneNumbersArrayByIdAsync(id,trackChanges);
            if (entity == null)
            {
                throw new OnNumaraNotFoundException(id);
            }

            _manager.OnNumara.DeleteOneNumbersArray(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<OnNumaraDto>> GetAllNumbersArraysAsync(bool trackChanges)
        {
            var entities = await _manager.OnNumara.GetAllNumbersArrayAsync(trackChanges);
            return _mapper.Map<IEnumerable<OnNumaraDto>>(entities);
        }

        public async Task<OnNumaraDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await _manager.OnNumara.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new OnNumaraNotFoundException(id);
            }
            return _mapper.Map<OnNumaraDto>(entity);
        }

        public async Task UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges)
        {
            if(onNumaraDtoForUpdate == null)
            {
                throw new ArgumentNullException(nameof(onNumaraDtoForUpdate));
            }
            var entity = await _manager.OnNumara.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new OnNumaraNotFoundException(id);
            }
            entity = _mapper.Map<OnNumara>(onNumaraDtoForUpdate);
            _manager.OnNumara.UpdateOneNumbersArray(entity);
            await _manager.SaveAsync();
        }

        public Task<List<int>> GetRondomNumbersAsync()
        {
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
            return randomNumbers;
        }

        private Task<List<int>> GenerateRandomNumbersAsync()
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

        private async Task<IEnumerable<SayisalLotoDto>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges)
        {
            var entities = await _manager.SayisalLoto.GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            return _mapper.Map<IEnumerable<SayisalLotoDto>>(entities);
        }

    }
}
