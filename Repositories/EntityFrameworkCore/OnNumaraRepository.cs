using Entities.Models;
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

        public IQueryable<OnNumara> GetAllNumbersArrayAsync(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(o=>o.Date);

        public OnNumara GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            FindByCondition(o => o.Id.Equals(id), trackChanges).SingleOrDefault();

        public void UpdateOneNumbersArray(OnNumara onNumara) => Update(onNumara);
    }
}
