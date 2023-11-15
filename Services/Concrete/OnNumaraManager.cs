using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class OnNumaraManager : IOnNumaraService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public OnNumaraManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public OnNumara CreateOneNumbersArrayAsync(OnNumara onNumara)
        {
            _manager.OnNumara.CreateOneNumbersArray(onNumara);
            _manager.Save();
            return onNumara;
        }

        public void DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = _manager.OnNumara.GetOneNumbersArrayByIdAsync(id,trackChanges);
            if (entity == null)
            {
                string message = $"The On Numara array with id : {id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message);
            }

            _manager.OnNumara.DeleteOneNumbersArray(entity);
            _manager.Save();
        }

        public IEnumerable<OnNumara> GetAllNumbersArraysAsync(bool trackChanges)
        {
            var entities = _manager.OnNumara.GetAllNumbersArrayAsync(trackChanges);
            return entities;
        }

        public OnNumara GetOneNumbersArrayByIdAsync(int id, bool trackChanges)
        {
            var entity = _manager.OnNumara.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new Exception($"The On Numara array with id : {id} could not found.");
            }
            return entity;
        }

        public void UpdateOneNumbersArrayAsync(int id, OnNumara onNumara, bool trackChanges)
        {
            if(onNumara == null)
            {
                throw new ArgumentNullException(nameof(onNumara));
            }
            var entity = _manager.OnNumara.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                string message = $"The On Numara array with id : {id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message);
            }

            entity.Numbers = onNumara.Numbers;
            entity.Date = onNumara.Date;

            _manager.OnNumara.UpdateOneNumbersArray(entity);
            _manager.Save();
        }
    }
}
