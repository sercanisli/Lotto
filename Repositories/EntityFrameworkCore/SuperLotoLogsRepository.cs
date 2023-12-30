using Entities.LogModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class SuperLotoLogsRepository : LogsRepositoryBase<SuperLotoLogs>, ISuperLotoLogsRepository
    {
        public SuperLotoLogsRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateLog(SuperLotoLogs superLotoLogs) => Create(superLotoLogs);

        public async Task<IEnumerable<SuperLotoLogs>> GetAllLogsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();
    }
}
