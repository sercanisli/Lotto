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

        public SuperLotoDto CreateOneNumbersArray(SuperLotoDtoForInsertion superLotoDto)
        {
            var entity = _mapper.Map<SuperLoto>(superLotoDto);
            _manager.SuperLoto.CreateOneNumbersArray(entity);
            _manager.Save();
            return _mapper.Map<SuperLotoDto>(entity);
        }

        public void DeleteOneNumbersArray(int id, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }
            _manager.SuperLoto.DeleteOneNumbersArray(entity);
            _manager.Save();
        }

        public IEnumerable<SuperLotoDto> GetAllNumbersArrays(bool trackChanges)
        {
            var entities = _manager.SuperLoto.GetAllNumbersArray(trackChanges);
            return _mapper.Map<IEnumerable<SuperLotoDto>>(entities);
        }

        public SuperLotoDto GetOneNumbersArrayById(int id, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, trackChanges);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }

            return _mapper.Map<SuperLotoDto>(entity);
        }

        public void UpdateOneNumbersArray(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }

            entity = _mapper.Map<SuperLoto>(superLotoDto);

            _manager.SuperLoto.UpdateOneNubersArray(entity);
            _manager.Save();
        }

        public IEnumerable<int> GetOnlyNumbers(bool trackChanges)
        {
            var entities = _manager.SuperLoto.GetAllNumbersArray(trackChanges).ToList();

            var numbers = entities.SelectMany(e => e.Numbers).ToList();

            return numbers;
        }

        public List<int> GetRondomNumbers()
        {
            List<int> randomNumbers = new List<int>();
            int i = 0;
            do
            {
                var numbers = GenerateRandomNumbers();
                if (AreTheNumbersTheSame(numbers)==true)
                {
                    i++;
                    randomNumbers=numbers;
                }
            } while (i == 0);
            return randomNumbers;
        }

        private List<int> GenerateRandomNumbers()
        {
            int sleepTimeInSeconds = 1;
            var numbers = GetOnlyNumbers(false);
            int totalCount = numbers.Count();
            long ticks = DateTime.Now.Ticks;
            Random random = new Random((int)ticks);
            var randomNumbers = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                Thread.Sleep(sleepTimeInSeconds);
                randomNumbers.Add(random.Next(1, totalCount));
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
    }
}
