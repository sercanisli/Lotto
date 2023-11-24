using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SansTopuManager : ISansTopuService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public SansTopuManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
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
            if(entity == null)
            {
                throw new Exception($"Sans Topu with id : {id} could not found");
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
            if(entity == null)
            {
                throw new Exception($"Sans Topu with id : {id} could not found");
            }
            return entity;
        }

        public void UpdateOneNumbersArray(int id, SansTopu sansTopu, bool trackChanges)
        {
            var entity = _manager.SansTopu.GetOneNumbersArrayById(id,trackChanges);
            if (entity == null)
            {
                throw new Exception($"Sans Topu with id : {id} could not found");
            }
            if (sansTopu == null)
            {
                throw new ArgumentNullException(nameof(sansTopu));
            }
            entity.Numbers = sansTopu.Numbers;
            entity.PlusNumber = sansTopu.PlusNumber;
            entity.Date = sansTopu.Date;
            _manager.SansTopu.Update(entity);
            _manager.Save();
        }
    }
}
