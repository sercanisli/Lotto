using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SayisalLotoManager : ISayisalLotoService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public SayisalLotoManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
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

        public async Task<(IEnumerable<SayisalLotoDto> sayisalLotos, MetaData metaData)> GetAllNumbersArraysAsync(SayisalLotoParameters sayisalLotoParameters, bool trackChanges)
        {
            var entitiesWithMetaData = await _manager.SayisalLoto.GetAllNumbersArrayAsync(sayisalLotoParameters,trackChanges);
            var sayisalLotosDto = _mapper.Map<IEnumerable<SayisalLotoDto>>(entitiesWithMetaData);
            return (sayisalLotosDto, entitiesWithMetaData.MetaData);
        }

        public async Task<SayisalLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<SayisalLotoDto>(entity);
        }

        public async Task UpdateOneNumbersArrayAsync(int id, SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate, bool trackChanges)
        {
            var entity = await GetOneNumbersArrayByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<SayisalLoto>(sayisalLotoDtoForUpdate);
            _manager.SayisalLoto.UpdateOneNumbersArray(entity);
            await _manager.SaveAsync();
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
    }
}
