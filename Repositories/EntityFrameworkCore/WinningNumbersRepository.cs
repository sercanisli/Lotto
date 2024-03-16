using Entities.Models;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class WinningNumbersRepository : RepositoryBase<WinningNumbers>, IWinningNumbersRepository
    {
        public WinningNumbersRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
