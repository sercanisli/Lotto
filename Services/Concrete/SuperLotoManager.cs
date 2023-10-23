using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SuperLotoManager : ISuperLotoService
    {
        private readonly IRepositoryManager _manager;

        public SuperLotoManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public SuperLoto CreateOneNumbersArray(SuperLoto superLoto)
        {
            if(superLoto == null)
            {
                throw new ArgumentNullException(nameof(superLoto));
            }
            _manager.SuperLoto.CreateOneNumbersArray(superLoto);
            _manager.Save();
            return superLoto;
        }

        public void DeleteOneNumbersArray(int id, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
            if(entity == null) 
            {
                throw new Exception($"Super Loto wit id: {id} could not found.");
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
                throw new Exception($"Super Loto wit id: {id} could not found.");
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
