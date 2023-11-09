using Entities.Models;
using Entities.RequestFeatures;
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

        public async Task<IEnumerable<SayisalLoto>> GetAllNumbersArrayAsync(SayisalLotoParameters sayisalLotoParameters, bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(sl=>sl.Numbers)
            .Skip((sayisalLotoParameters.PageNumber-1)*sayisalLotoParameters.PageSize)
            .Take(sayisalLotoParameters.PageSize)
            .ToListAsync();

        public async Task<SayisalLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(sl => sl.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNumbersArray(SayisalLoto sayisalLoto) => Update(sayisalLoto);
    }
}
