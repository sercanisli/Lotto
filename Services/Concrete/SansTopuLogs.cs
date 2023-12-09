using AutoMapper;
using Entities.DataTransferObjects;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class SansTopuLogs : ISansTopuLogsService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public SansTopuLogs(IRepositoryManager manager, IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<SansTopuDtoForRandom> CreateLog(SansTopuDtoForRandom sansTopuDtoForRandom)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SansTopuDtoForRandom>> GetAllLogsAsync(bool trackChanges)
        {
            var logs = _manager.SansTopuLogs.FindAll(trackChanges).ToList();

            //mapping
            //return 
        }
    }
}
