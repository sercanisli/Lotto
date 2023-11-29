using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
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
        private readonly IDataShaper<SansTopuDto> _shaper;

        public SansTopuManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<SansTopuDto> shaper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _shaper = shaper;
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

        public async Task<(IEnumerable<ShapedEntity> sansTopuDto, MetaData metaData )> GetAllNumbersArraysAsync(SansTopuParameters sansTopuParameters, bool trackChanges)
        {
            var entitiesWithMetaData = await _manager.SansTopu.GetAllNumbersArrayAsync(sansTopuParameters, trackChanges);
            var stDto = _mapper.Map<IEnumerable<SansTopuDto>>(entitiesWithMetaData);
            var shapedData = _shaper.ShapeData(stDto, sansTopuParameters.Fields);
            return (sansTopuDto: shapedData, metaData: entitiesWithMetaData.MetaData);
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

        private async Task<SansTopu> GetOneNumbersArrayByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.SansTopu.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            return entity;
        }
    }
}
