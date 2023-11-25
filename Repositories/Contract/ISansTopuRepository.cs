using Entities.Models;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface ISansTopuRepository : IRepositoryBase<SansTopu>
    {
        Task<IEnumerable<SansTopu>> GetAllNumbersArrayAsync(bool trackChanges);
        Task<SansTopu> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(SansTopu sansTopu);
        void UpdateOneNumbersArray(SansTopu sansTopu);
        void DeleteOneNumbersArray(SansTopu sansTopu);
    }
}
