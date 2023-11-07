using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
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

        public SayisalLoto CreateOneNumbersArrayAsync(SayisalLoto sayisalLoto)
        {
            _manager.SayisalLoto.CreateOneNumbersArray(sayisalLoto);
            _manager.Save();
            return sayisalLoto;
        }

        public void DeleteOneNumbersArrayAsync(int id, bool trackChanges)
        {
            var entity = _manager.SayisalLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new SayisalLotoNotFoundException(id);
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
            var entity = _manager.SayisalLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new SayisalLotoNotFoundException(id);
            }
            return entity;
        }

        public void UpdateOneNumbersArrayAsync(int id, SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate, bool trackChanges)
        {
            var entity = _manager.SayisalLoto.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new SayisalLotoNotFoundException(id);
            }
            entity = _mapper.Map<SayisalLoto>(sayisalLotoDtoForUpdate);

            _manager.SayisalLoto.UpdateOneNumbersArray(entity);
            _manager.Save();
        }
    }
}
