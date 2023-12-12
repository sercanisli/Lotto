using Entities.LogModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class OnNumaraLogsRepository : LogsRepositoryBase<OnNumaraLogs>, IOnNumaraLogsRepository
    {
        public OnNumaraLogsRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateLog(OnNumaraLogs onNumaraLogs) => Create(onNumaraLogs);

        public async Task<IEnumerable<OnNumaraLogs>> GetAllLogsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();
    }
}
