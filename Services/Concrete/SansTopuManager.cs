﻿using AutoMapper;
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

        public SansTopuDto CreateOneNumbersArray(SansTopuDtoForInsertion sansTopuDtoForInsertion)
        {
            if(sansTopuDtoForInsertion == null)
            {
                throw new ArgumentNullException(nameof(sansTopuDtoForInsertion));
            }
            var entity = _mapper.Map<SansTopu>(sansTopuDtoForInsertion);
            _manager.SansTopu.CreateOneNumbersArray(entity);
            _manager.Save();
            return _mapper.Map<SansTopuDto>(entity);
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

        public IEnumerable<SansTopuDto> GetAllNumbersArrays(bool trackChanges)
        {
            var entities = _manager.SansTopu.GetAllNumbersArray(trackChanges);
            return _mapper.Map<IEnumerable<SansTopuDto>>(entities);
        }

        public SansTopuDto GetOneNumbersArrayById(int id, bool trackChanges)
        {
            var entity = _manager.SansTopu.GetOneNumbersArrayById(id, trackChanges);
            if (entity == null)
            {
                throw new SansTopuNotFoundExceptions(id);
            }
            return _mapper.Map<SansTopuDto>(entity);
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
