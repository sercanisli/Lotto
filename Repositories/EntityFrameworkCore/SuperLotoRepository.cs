using Entities.Models;
using Repositories.Cantracts;

namespace Repositories.EntityFrameworkCore
{
    public class SuperLotoRepository : RepositoryBase<SuperLoto>, ISuperLotoRepository
    {
        public SuperLotoRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneNumbersArray(SuperLoto superLoto) => Create(superLoto);

        public void DeleteOneNumbersArray(SuperLoto superLoto) => Delete(superLoto);

        public IQueryable<SuperLoto> GetAllNumbersArray(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<SuperLoto> GetOneNumbersArrayById(int id, bool trackChanges) =>
            FindByCondition(sl => sl.Id == id, trackChanges);

        public void UpdateOneNubersArray(SuperLoto superLoto) => Update(superLoto);
    }
}
