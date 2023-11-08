using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class SayisalLotoRepository : RepositoryBase<SayisalLoto>, ISayisalLotoRepository
    {
        public SayisalLotoRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneNumbersArray(SayisalLoto sayisalLoto) => Create(sayisalLoto);

        public void DeleteOneNumbersArray(SayisalLoto sayisalLoto) => Delete(sayisalLoto);
        public async Task<IEnumerable<SayisalLoto>> GetAllNumbersArrayAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(sl=>sl.Numbers).ToListAsync();
        public async Task<SayisalLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(sl => sl.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNumbersArray(SayisalLoto sayisalLoto) => Update(sayisalLoto);
    }
}
