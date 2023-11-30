using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface ISansTopuRepository : IRepositoryBase<SansTopu>
    {
        Task<PagedList<SansTopu>> GetAllNumbersArrayAsync(SansTopuParameters sansTopuParameters ,bool trackChanges);
        Task<IEnumerable<SansTopu>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges);
        Task<SansTopu> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(SansTopu sansTopu);
        void UpdateOneNumbersArray(SansTopu sansTopu);
        void DeleteOneNumbersArray(SansTopu sansTopu);
    }
}
