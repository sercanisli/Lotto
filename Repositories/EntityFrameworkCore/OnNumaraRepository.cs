using Entities.Models;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class OnNumaraRepository : RepositoryBase<OnNumara>, IOnNumaraRepository
    {
        public OnNumaraRepository(RepositoryContext context) : base(context)
        {

        }
    }
}
