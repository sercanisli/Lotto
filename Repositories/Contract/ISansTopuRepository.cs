using Entities.Models;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface ISansTopuRepository : IRepositoryBase<SansTopu>
    {
        IQueryable<SansTopu> GetAllNumbersArray(bool trackChanges);
        SansTopu GetOneNumbersArrayById(int id, bool trackChanges);
        void CreateOneNumbersArray(SansTopu sansTopu);
        void UpdateOneNumbersArray(SansTopu sansTopu);
        void DeleteOneNumbersArray(SansTopu sansTopu);
    }
}
