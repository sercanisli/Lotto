using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class SansTopuRepository : RepositoryBase<SansTopu>, ISansTopuRepository
    {
        public SansTopuRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneNumbersArray(SansTopu sansTopu) => Create(sansTopu);

        public void DeleteOneNumbersArray(SansTopu sansTopu) => Delete(sansTopu);

        public async Task<IEnumerable<SansTopu>> GetAllNumbersArrayAsync(SansTopuParameters sansTopuParameters, bool trackChanges) =>
           await FindAll(trackChanges)
            .OrderBy(st=>st.Date)
            .Skip((sansTopuParameters.PageNumber-1)*sansTopuParameters.PageSize)
            .Take(sansTopuParameters.PageSize)
            .ToListAsync();

        public async Task<SansTopu> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(st => st.Id == id, trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNumbersArray(SansTopu sansTopu) => Update(sansTopu);
    }
}
