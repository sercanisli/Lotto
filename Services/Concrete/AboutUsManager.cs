﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class AboutUsManager : IAboutUsService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly ICacheService _cache;

        public AboutUsManager(IRepositoryManager manager, IMapper mapper, ILoggerService logger, ICacheService cache)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<AboutUsDto> CreateOneAboutUsAsync(AboutUsDto aboutUsDto)
        {
            var entity = _mapper.Map<AboutUs>(aboutUsDto);
            _manager.AboutUs.Create(entity);
            await _manager.SaveAsync();
            _logger.LogInfo($"{entity.Id} - About Us Description added to DB");
            SetCache<AboutUs>($"about-us-{entity.Id}", entity);
            return _mapper.Map<AboutUsDto>(entity);
        }

        public async Task DeleteAboutUsAsync(int id, bool trackChanges)
        {
            var entity = await GetOneAboutUsAsync(id, trackChanges);
            if (entity == null)
            {
                throw new Exception($"About Us with id {id} could not found.");
            }
            var mappedEntity = _mapper.Map<AboutUs>(entity);
            _manager.AboutUs.Delete(mappedEntity);
            _logger.LogInfo($"About Us with id : {id} deleted");
            _cache.RemoveData($"about-us-{id}");
            await _manager.SaveAsync();
        }

        public async Task<AboutUsDto> GetOneAboutUsAsync(int id, bool trackchanges)
        {
            var cachedData = _cache.GetData<AboutUs>($"about-us-{id}");
            if(cachedData != null)
            {
                return _mapper.Map<AboutUsDto>(cachedData);
            }
            var entity = await _manager.AboutUs.FindByCondition(au => au.Id == id, trackchanges).SingleOrDefaultAsync();
            if(entity == null)
            {
                throw new Exception($"About Us with id {id} could not found.");
            }
            SetCache<AboutUs>($"about-us-{id}", entity);
            _logger.LogInfo($"Get request made to About Us with id : {id} ");
            return _mapper.Map<AboutUsDto>(entity);
        }

        public async Task UpdateAboutUsAsync(int id, AboutUsDto aboutUsDto, bool trackchanges)
        {
            var entity = await GetOneAboutUsAsync(id, trackchanges);
            if (entity == null)
            {
                throw new Exception($"about Us with id : {id} could not found");
            }
            var mappedEntity = _mapper.Map<AboutUs>(entity);
            _manager.AboutUs.Update(mappedEntity);
            _logger.LogInfo($"About Us with id : {id} has been updated");
            SetCache<AboutUs>($"about-us-{id}", mappedEntity);
            await _manager.SaveAsync();
        }

        private void SetCache<T>(string key, T value)
        {
            var expiryTime = DateTimeOffset.Now.AddSeconds(120);
            _cache.SetData(key, value, expiryTime);
        }
    }
}
