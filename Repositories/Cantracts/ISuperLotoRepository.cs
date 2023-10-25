using Entities.Models;

namespace Repositories.Cantracts
{
    public interface ISuperLotoRepository : IRepositoryBase<SuperLoto>
    {
        Task<IEnumerable<SuperLoto>> GetAllNumbersArrayAsync(bool trackChanges);
        Task<SuperLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(SuperLoto superLoto);
        void UpdateOneNubersArray(SuperLoto superLoto);
        void DeleteOneNumbersArray(SuperLoto superLoto);
    }
}
