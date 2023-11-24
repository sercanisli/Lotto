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

        public SansTopu CreateOneNumbersArray(SansTopu sansTopu)
        {
            if(sansTopu == null)
            {
                throw new ArgumentNullException(nameof(sansTopu));
            }
            _manager.SansTopu.CreateOneNumbersArray(sansTopu);
            _manager.Save();
            return sansTopu;
        }

        public void DeleteOneNumbersArray(int id, bool trackChanges)
        {
            var entity = _manager.SansTopu.GetOneNumbersArrayById(id,trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            _manager.SansTopu.DeleteOneNumbersArray(entity);
            _manager.Save();
        }

        public IEnumerable<SansTopu> GetAllNumbersArrays(bool trackChanges)
        {
            var entities = _manager.SansTopu.GetAllNumbersArray(trackChanges);
            return entities;
        }

        public SansTopu GetOneNumbersArrayById(int id, bool trackChanges)
        {
            var entity = _manager.SansTopu.GetOneNumbersArrayById(id, trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            return entity;
        }

        public void UpdateOneNumbersArray(int id, SansTopuDtoForUpdate sansTopuDtoForUpdate, bool trackChanges)
        {
            var entity = _manager.SansTopu.GetOneNumbersArrayById(id,trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            if (sansTopuDtoForUpdate == null)
            {
                throw new ArgumentNullException(nameof(sansTopuDtoForUpdate));
            }
            entity = _mapper.Map<SansTopu>(sansTopuDtoForUpdate);
            _manager.SansTopu.Update(entity);
            _manager.Save();
        }
    }
}
