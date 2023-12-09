using Entities.Models;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class SansTopuLogsRepository : LogsRepositoryBase<SansTopu>, ISansTopuLogsRepository
    {
        public SansTopuLogsRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
