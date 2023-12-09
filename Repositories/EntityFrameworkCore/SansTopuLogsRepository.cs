using Entities.LogModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class SansTopuLogsRepository : LogsRepositoryBase<SansTopuLogs>, ISansTopuLogsRepository
    {
        public SansTopuLogsRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateLog(SansTopuLogs sansTopuLogs) => Create(sansTopuLogs);

        public async Task<IEnumerable<SansTopuLogs>> GetAllLogsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();
    }
    }
}
