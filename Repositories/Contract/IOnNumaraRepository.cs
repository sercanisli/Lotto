using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface IOnNumaraRepository : IRepositoryBase<OnNumara>
    {
        Task<IEnumerable<OnNumara>> GetAllNumbersArrayAsync(bool trackChanges);
        Task<IEnumerable<OnNumara>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges);
        Task<OnNumara> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(OnNumara onNumara);
        void UpdateOneNumbersArray(OnNumara onNumara);
        void DeleteOneNumbersArray(OnNumara onNumara);
    }
}
