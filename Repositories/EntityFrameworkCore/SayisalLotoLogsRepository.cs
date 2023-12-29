using Entities.LogModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class SayisalLotoLogsRepository : RepositoryBase<SayisalLotoLogs>, ISayisalLotoLogsRepository
    {
        public SayisalLotoLogsRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateLog(SayisalLotoLogs sayisalLotoLogs) => Create(sayisalLotoLogs);

        public async Task<IEnumerable<SayisalLotoLogs>> GetAllLogsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();
    }
}
