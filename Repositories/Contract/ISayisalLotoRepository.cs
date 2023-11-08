using Entities.Models;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface ISayisalLotoRepository : IRepositoryBase<SayisalLoto>
    {
        Task<IEnumerable<SayisalLoto>> GetAllNumbersArrayAsync(bool trackChanges);
        Task<SayisalLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(SayisalLoto sayisalLoto);
        void UpdateOneNumbersArray(SayisalLoto sayisalLoto);
        void DeleteOneNumbersArray(SayisalLoto sayisalLoto);
    }
}
