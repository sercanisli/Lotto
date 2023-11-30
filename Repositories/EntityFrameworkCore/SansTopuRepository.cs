using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;
using Repositories.EntityFrameworkCore.Extensions;

namespace Repositories.EntityFrameworkCore
{
    public class SansTopuRepository : RepositoryBase<SansTopu>, ISansTopuRepository
    {
        public SansTopuRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneNumbersArray(SansTopu sansTopu) => Create(sansTopu);

        public void DeleteOneNumbersArray(SansTopu sansTopu) => Delete(sansTopu);

        public async Task<PagedList<SansTopu>> GetAllNumbersArrayAsync(SansTopuParameters sansTopuParameters, bool trackChanges)
        {
            var entities = await FindAll(trackChanges)
                .Sort(sansTopuParameters.OrderBy)
                .ToListAsync();

            return PagedList<SansTopu>.ToPagedList(entities, sansTopuParameters.PageNumber, sansTopuParameters.PageSize);
        }

        public async Task<IEnumerable<SansTopu>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges) => 
            await FindAll(trackChanges).ToListAsync();

        public async Task<SansTopu> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(st => st.Id == id, trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNumbersArray(SansTopu sansTopu) => Update(sansTopu);
    }
}
