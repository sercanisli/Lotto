using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SuperLotoManager : ISuperLotoService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public SuperLotoManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public SuperLoto CreateOneNumbersArray(SuperLoto superLoto)
        {
            _manager.SuperLoto.CreateOneNumbersArray(superLoto);
            _manager.Save();
            return superLoto;
        }

        public void DeleteOneNumbersArray(int id, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
            if(entity == null) 
            {
                string message = $"Super Loto wit id: {id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
            _manager.SuperLoto.DeleteOneNumbersArray(entity);
            _manager.Save();
        }

        public IEnumerable<SuperLoto> GetAllNumbersArrays(bool trackChanges)
        {
            return _manager.SuperLoto.GetAllNumbersArray(trackChanges); 
        }

        public SuperLoto GetOneNumbersArrayById(int id, bool trackChanges)
        {
            return _manager.SuperLoto.GetOneNumbersArrayById(id, trackChanges);
        }

        public void UpdateOneNumbersArray(int id, SuperLoto superLoto, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
            if (entity == null)
            {
                string message = $"Super Loto wit id: {id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
            if (superLoto == null)
            {
                throw new ArgumentNullException(nameof(superLoto));
            }
            entity.Numbers=superLoto.Numbers;

            _manager.SuperLoto.UpdateOneNubersArray(entity);
            _manager.Save();
        }
    }
}
