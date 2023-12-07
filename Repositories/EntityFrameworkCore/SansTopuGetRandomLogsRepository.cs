using Entities.Models;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class SansTopuGetRandomLogsRepository : RepositoryBase<SansTopuGetRandomLogs>, ISansTopuGetRandomLogsRepository
    {
        public SansTopuGetRandomLogsRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
