using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SuperLotoManager : ISuperLotoService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public SuperLotoManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
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
            var entity = await _manager.SuperLoto.GetOneNumbersArrayByIdAsync(id, false);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }
            _manager.SuperLoto.DeleteOneNumbersArray(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<SuperLotoDto>> GetAllNumbersArraysAsync(bool trackChanges)
        {
            var entities = await _manager.SuperLoto.GetAllNumbersArrayAsync(trackChanges);
            return _mapper.Map<IEnumerable<SuperLotoDto>>(entities);
        }

        public async Task<SuperLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await _manager.SuperLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }
            return _mapper.Map<SuperLotoDto>(entity);
        }

        public async Task UpdateOneNumbersArrayAsync(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges)
        {
            var entity = await _manager.SuperLoto.GetOneNumbersArrayByIdAsync(id, false);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }

            entity = _mapper.Map<SuperLoto>(superLotoDto);

            _manager.SuperLoto.UpdateOneNubersArray(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<int>> GetOnlyNumbersAsync(bool trackChanges)
        {
            var entities = await _manager.SuperLoto.GetAllNumbersArrayAsync(trackChanges);

            var numbers = entities.SelectMany(e => e.Numbers).ToList();

            numbers = Sort(numbers);

            return numbers;
        }

        public async Task<List<int>> GetRondomNumbersAsync()
        {
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
            return randomNumbers;
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
        
    }
}
