using Entities.Models;
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
        public IQueryable<SayisalLoto> GetAllNumbersArrayAsync(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(sl=>sl.Numbers);
        public IQueryable<SayisalLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            FindByCondition(sl => sl.Id.Equals(id), trackChanges);

        public void UpdateOneNumbersArray(SayisalLoto sayisalLoto) => Update(sayisalLoto);
    }
}
