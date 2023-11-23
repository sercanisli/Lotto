using Entities.Models;
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

        public IQueryable<SansTopu> GetAllNumbersArray(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(st=>st.Date);

        public SansTopu GetOneNumbersArrayById(int id, bool trackChanges) =>
            FindByCondition(st => st.Id.Equals(id), trackChanges).SingleOrDefault();

        public void UpdateOneNumbersArray(SansTopu sansTopu) => Update(sansTopu);
    }
}
