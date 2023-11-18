using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;
using Repositories.EntityFrameworkCore.Extensions;

namespace Repositories.EntityFrameworkCore
{
    public class OnNumaraRepository : RepositoryBase<OnNumara>, IOnNumaraRepository
    {
        public OnNumaraRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneNumbersArray(OnNumara onNumara) => Create(onNumara);

        public void DeleteOneNumbersArray(OnNumara onNumara) => Delete(onNumara);

        public async Task<PagedList<OnNumara>> GetAllNumbersArrayAsync(OnNumaraParameters onNumaraParameters, bool trackChanges)
        {
            var entities = await FindAll(trackChanges)
                .Sort(onNumaraParameters.OrderBy)
                .ToListAsync();
            return PagedList<OnNumara>
                .ToPagedList(entities, onNumaraParameters.PageNumber, onNumaraParameters.PageSize);
        }

        public async Task<IEnumerable<OnNumara>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<OnNumara> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(o => o.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNumbersArray(OnNumara onNumara) => Update(onNumara);
    }
}
