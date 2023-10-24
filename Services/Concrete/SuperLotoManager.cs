using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SuperLotoManager : ISuperLotoService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public SuperLotoManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public SuperLotoDto CreateOneNumbersArray(SuperLotoDtoForInsertion superLotoDto)
        {
            var entity = _mapper.Map<SuperLoto>(superLotoDto);
            _manager.SuperLoto.CreateOneNumbersArray(entity);
            _manager.Save();
            return _mapper.Map<SuperLotoDto>(entity);
        }

        public void DeleteOneNumbersArray(int id, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }
            _manager.SuperLoto.DeleteOneNumbersArray(entity);
            _manager.Save();
        }

        public IEnumerable<SuperLotoDto> GetAllNumbersArrays(bool trackChanges)
        {
            var entities = _manager.SuperLoto.GetAllNumbersArray(trackChanges);
            return _mapper.Map<IEnumerable<SuperLotoDto>>(entities);
        }

        public SuperLotoDto GetOneNumbersArrayById(int id, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, trackChanges);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }

            return _mapper.Map<SuperLotoDto>(entity);
        }

        public void UpdateOneNumbersArray(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges)
        {
            var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
            if (entity == null)
            {
                throw new SuperLotoNotFoundException(id);
            }

            entity = _mapper.Map<SuperLoto>(superLotoDto);

            _manager.SuperLoto.UpdateOneNubersArray(entity);
            _manager.Save();
        }
    }
}
