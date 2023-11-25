using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SansTopuManager : ISansTopuService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public SansTopuManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
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
            var entity = await _manager.SansTopu.GetOneNumbersArrayByIdAsync(id,trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            _manager.SansTopu.DeleteOneNumbersArray(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<SansTopuDto>> GetAllNumbersArraysAsync(bool trackChanges)
        {
            var entities = await _manager.SansTopu.GetAllNumbersArrayAsync(trackChanges);
            return _mapper.Map<IEnumerable<SansTopuDto>>(entities);
        }

        public async Task<SansTopuDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await _manager.SansTopu.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            return _mapper.Map<SansTopuDto>(entity);
        }

        public async Task UpdateOneNumbersArrayAsync(int id, SansTopuDtoForUpdate sansTopuDtoForUpdate, bool trackChanges)
        {
            var entity = await _manager.SansTopu.GetOneNumbersArrayByIdAsync(id,trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            entity = _mapper.Map<SansTopu>(sansTopuDtoForUpdate);
            _manager.SansTopu.Update(entity);
            await _manager.SaveAsync();
        }
    }
}
