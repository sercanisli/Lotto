using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
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
        private readonly IDataShaper<SuperLotoDto> _shaper;

        public SuperLotoManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<SuperLotoDto> shaper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _shaper = shaper;
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

        public async Task<(IEnumerable<ExpandoObject> superLotoDto, MetaData metaData)> GetAllNumbersArraysAsync(SuperLotoParameters superLotoParameters, bool trackChanges)
        {
            var entitiesWithMetaData = await _manager.SuperLoto.GetAllNumbersArrayAsync(superLotoParameters ,trackChanges);
            var sLDto = _mapper.Map<IEnumerable<SuperLotoDto>>(entitiesWithMetaData);
            var shapedData = _shaper.ShapeData(sLDto, superLotoParameters.Fields);
            return (superLotoDto: shapedData, metaData: entitiesWithMetaData.MetaData);
        }

        public async Task<SuperLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<SuperLotoDto>(entity);
        }

        public async Task<SuperLotoDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges)
        {
            var entities = await GetAllNumbersArrayAsyncWithoutPaginationAsync(trackChanges);
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
            var entities = await GetAllNumbersArrayAsyncWithoutPaginationAsync(trackChanges);
            var numbers = entities.SelectMany(e => e.Numbers).ToList();
            return numbers;
        }

        private async Task<IEnumerable<SuperLotoDto>> GetAllNumbersArrayAsyncWithoutPaginationAsync(bool trackChanges)
        {
            var entities = await _manager.SuperLoto.GetAllNumbersArrayAsyncWithoutPaginationAsync(trackChanges);
            return _mapper.Map<IEnumerable<SuperLotoDto>>(entities);
        }
    }
}
