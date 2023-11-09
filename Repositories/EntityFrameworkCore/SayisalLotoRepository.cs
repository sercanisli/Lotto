using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;
using Repositories.EntityFrameworkCore.Extensions;

namespace Repositories.EntityFrameworkCore
{
    public class SayisalLotoRepository : RepositoryBase<SayisalLoto>, ISayisalLotoRepository
    {
        public SayisalLotoRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneNumbersArray(SayisalLoto sayisalLoto) => Create(sayisalLoto);

        public void DeleteOneNumbersArray(SayisalLoto sayisalLoto) => Delete(sayisalLoto);

        public async Task<PagedList<SayisalLoto>> GetAllNumbersArrayAsync(SayisalLotoParameters sayisalLotoParameters, bool trackChanges)
        {
            var entities = await FindAll(trackChanges)
                .Sort(sayisalLotoParameters.OrderBy)
                .ToListAsync();
                return PagedList<SayisalLoto>.ToPagedList(entities, sayisalLotoParameters.PageNumber, sayisalLotoParameters.PageSize);
        }
            
        public async Task<SayisalLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(sl => sl.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNumbersArray(SayisalLoto sayisalLoto) => Update(sayisalLoto);
    }
}
