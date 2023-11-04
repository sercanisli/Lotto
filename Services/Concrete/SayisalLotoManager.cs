using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SayisalLotoManager : ISayisalLotoService
    {
        private readonly IRepositoryManager _manager;

        public SayisalLotoManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public SayisalLoto CreateOneNumbersArrayAsync(SayisalLoto sayisalLoto)
        {
            if(sayisalLoto == null)
            {
                throw new ArgumentNullException(nameof(sayisalLoto));
            }

            _manager.SayisalLoto.CreateOneNumbersArray(sayisalLoto);
            _manager.SaveAsync();
            return sayisalLoto;
        }

        public void DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = _manager.SayisalLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if(entity == null)
            {
                throw new Exception($"Sayisal Loto with id : {id} could not found.");
            }
            _manager.SayisalLoto.DeleteOneNumbersArray(entity);
            _manager.SaveAsync();
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
            if (entity == null)
            {
                throw new Exception($"Sayisal Loto with id : {id} could not found.");
            }
            if(sayisalLoto  == null)
            {
                throw new ArgumentNullException(nameof(sayisalLoto));
            }
            entity.Numbers=sayisalLoto.Numbers;
            entity.Date = sayisalLoto.Date;

            _manager.SayisalLoto.UpdateOneNumbersArray(entity);
            _manager.SaveAsync();
        }
    }
}
