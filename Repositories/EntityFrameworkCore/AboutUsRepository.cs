using Entities.Models;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class AboutUsRepository : RepositoryBase<AboutUs>, IAboutUsRepository
    {
        public AboutUsRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
