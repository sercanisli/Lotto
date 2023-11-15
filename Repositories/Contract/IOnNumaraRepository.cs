using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface IOnNumaraRepository : IRepositoryBase<OnNumara>
    {
        IQueryable<OnNumara> GetAllNumbersArrayAsync(bool trackChanges);
        OnNumara GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(OnNumara onNumara);
        void UpdateOneNumbersArray(OnNumara onNumara);
        void DeleteOneNumbersArray(OnNumara onNumara);
    }
}
