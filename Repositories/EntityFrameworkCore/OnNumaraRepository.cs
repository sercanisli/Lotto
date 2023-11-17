using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class OnNumaraRepository : RepositoryBase<OnNumara>, IOnNumaraRepository
    {
        public OnNumaraRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneNumbersArray(OnNumara onNumara) => Create(onNumara);

        public void DeleteOneNumbersArray(OnNumara onNumara) => Delete(onNumara);

        public async Task<IEnumerable<OnNumara>> GetAllNumbersArrayAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(o => o.Date).ToListAsync();

        public async Task<IEnumerable<OnNumara>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<OnNumara> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(o => o.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNumbersArray(OnNumara onNumara) => Update(onNumara);
    }
}
