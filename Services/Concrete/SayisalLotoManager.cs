using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SayisalLotoManager : ISayisalLotoService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public SayisalLotoManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public SayisalLoto CreateOneNumbersArrayAsync(SayisalLoto sayisalLoto)
        {
            _manager.SayisalLoto.CreateOneNumbersArray(sayisalLoto);
            _manager.Save();
            return sayisalLoto;
        }

        public void DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = _manager.SayisalLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            string message = $"The sayisal loto with id : {id} could not found.";
            if (entity == null)
            {
                _logger.LogInfo(message);
                throw new Exception(message);
            }
            _manager.SayisalLoto.DeleteOneNumbersArray(entity);
            _manager.Save();
        }

        public IEnumerable<SayisalLoto> GetAllNumbersArraysAsync(bool trackChanges)
        {
            return _manager.SayisalLoto.GetAllNumbersArrayAsync(trackChanges);
        }

        public SayisalLoto GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            return _manager.SayisalLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
        }

        public void UpdateOneNumbersArrayAsync(int id, SayisalLoto sayisalLoto, bool trackChanges)
        {
            var entity = _manager.SayisalLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            string message = $"The sayisal loto with id : {id} could not found.";
            if (entity == null)
            {
                _logger.LogInfo(message);
                throw new Exception(message);
            }
            if(sayisalLoto  == null)
            {
                throw new ArgumentNullException(nameof(sayisalLoto));
            }
            entity.Numbers=sayisalLoto.Numbers;
            entity.Date = sayisalLoto.Date;

            _manager.SayisalLoto.UpdateOneNumbersArray(entity);
            _manager.Save();
        }
    }
}
