using AutoMapper;
using Entities.DataTransferObjects;
using Entities.LogModels;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SansTopuLogsManager : ISansTopuLogsService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public SansTopuLogsManager(IRepositoryManager manager, IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<SansTopuDtoForRandom> CreateLog(SansTopuDtoForRandom sansTopuDtoForRandom)
        {
            var entity = _mapper.Map<SansTopuLogs>(sansTopuDtoForRandom);
            _manager.SansTopuLogs.CreateLog(entity);
            await _manager.SaveAsync();
            return _mapper.Map<SansTopuDtoForRandom>(entity);
        }

        public async Task<IEnumerable<SansTopuDtoForRandom>> GetAllLogsAsync(bool trackChanges)
        {
            var logs = await _manager.SansTopuLogs.GetAllLogsAsync(trackChanges);
            var sansTopuLogs = _mapper.Map<IEnumerable<SansTopuDtoForRandom>>(logs);
            return sansTopuLogs;
        }
    }
}
