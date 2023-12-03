using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SansTopuManager : ISansTopuService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly ISansTopuLinks _sansTopuLinks;

        public SansTopuManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, ISansTopuLinks sansTopuLinks)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _sansTopuLinks = sansTopuLinks;
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

        public async Task<(List<int> numbers, int plusNumber)> GetRondomNumbersAsync()
        {
            List<int> randomNumbers = new List<int>();
            int i = 0;
            do
            {
                var plusNumber = await GenerateRandomPlusNumberAsync();
                var numbers = await GenerateRandomNumbersAsync();
                if (AreTheNumbersTheSame(numbers) == true)
                {
                    i++;
                    randomNumbers = numbers;
                }
            } while (i == 0);
            randomNumbers = Sort(randomNumbers);
            return (numbers:randomNumbers, plusNumber:randomPlusNumber);
        }

        private async Task<int> GenerateRandomPlusNumberAsync()
        {
            int index;
            int randomPlusNumber;
            int sleepTimeInSeconds = 1;
            var plusNumbers = await GetOnlyPlusNumbersAsync(false);
            int totalCount = plusNumbers.Count();
            long ticks = DateTime.Now.Ticks;

            Random random = new Random((int)ticks);
            Thread.Sleep(sleepTimeInSeconds);
            index = random.Next(0, totalCount - 1);
            randomPlusNumber = plusNumbers.ElementAt(index);

            return randomPlusNumber;
        }

        private async Task<List<int>> GetOnlyPlusNumbersAsync(bool trackChanges)
        {
            var entities = await GetAllNumbersArrayWithoutPaginationAsync(trackChanges);
            var plusNumbers = entities.Select(pl => pl.PlusNumber).ToList();
            return plusNumbers;
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
                index = random.Next(0, totalCount - 1);
                selectedNumber = numbers.ElementAt(index);
                randomNumbers.Add(selectedNumber);
            }
            return randomNumbers.ToList();
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
