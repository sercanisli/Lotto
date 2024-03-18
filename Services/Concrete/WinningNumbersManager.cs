﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class WinningNumbersManager : IWinningNumbersService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public WinningNumbersManager(IRepositoryManager manager, IMapper mapper, ILoggerService logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<WinnigNumbersDto> CreateOneWinningNumbersAsync(WinnigNumbersDto winnigNumbersDto)
        {
            var entity = _mapper.Map<WinningNumbers>(winnigNumbersDto);
            _manager.WinningNumbers.Create(entity);
            await _manager.SaveAsync();
            _logger.LogInfo($"{entity.Id} - Winning Numbers Description added to DB");
            return _mapper.Map<WinnigNumbersDto>(entity);
        }

        public async Task DeleteWinningNumbersAsync(int id, bool trackChanges)
        {
            var entity = await GetOneWinningNumbersAsync(id, trackChanges);
            if (entity == null)
            {
                throw new Exception($"WinningNumbers with id {id} could not found.");
            }
            var mappedEntity = _mapper.Map<WinningNumbers>(entity);
            _manager.WinningNumbers.Delete(mappedEntity);
            _logger.LogInfo($"WinningNumbers with id : {id} deleted");
            await _manager.SaveAsync();
        }

        public async Task<WinnigNumbersDto> GetOneWinningNumbersAsync(int id, bool trackchanges)
        {
            var entity = await _manager.WinningNumbers.FindByCondition(w => w.Id == id, trackchanges).SingleOrDefaultAsync();
            if(entity == null)
            {
                throw new Exception($"WinningNumbers with id {id} could not found.");
            }
            _logger.LogInfo($"Get request made to WinningNumbers with id : {id} ");
            return _mapper.Map<WinnigNumbersDto>(entity);
        }

        public async Task UpdateWinningNumbersAsync(int id, WinnigNumbersDto winnigNumbersDto, bool trackchanges)
        {
            var entity = await GetOneWinningNumbersAsync(id, trackchanges);
            if (entity == null)
            {
                throw new Exception($"WinningNumbers with id : {id} could not found");
            }

            var mappedEntity = _mapper.Map<WinningNumbers>(entity);
            _manager.WinningNumbers.Update(mappedEntity);
            _logger.LogInfo($"WinningNumbers with id : {id} has been updated");
            await _manager.SaveAsync();
        }
    }
}