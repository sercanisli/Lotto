using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class OnNumaraManager : IOnNumaraService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public OnNumaraManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
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
                throw new OnNumaraNotFoundException(id);
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
                throw new OnNumaraNotFoundException(id);
            }
            return entity;
        }

        public void UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges)
        {
            if(onNumaraDtoForUpdate == null)
            {
                throw new ArgumentNullException(nameof(onNumaraDtoForUpdate));
            }
            var entity = _manager.OnNumara.GetOneNumbersArrayByIdAsync(id, trackChanges);
            if (entity == null)
            {
                throw new OnNumaraNotFoundException(id);
            }
            entity = _mapper.Map<OnNumara>(onNumaraDtoForUpdate);
            _manager.OnNumara.UpdateOneNumbersArray(entity);
            _manager.Save();
        }
    }
}
