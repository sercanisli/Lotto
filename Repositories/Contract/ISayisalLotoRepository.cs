using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface ISayisalLotoRepository : IRepositoryBase<SayisalLoto>
    {
        Task<PagedList<SayisalLoto>> GetAllNumbersArrayAsync(SayisalLotoParameters sayisalLotoParameters, bool trackChanges);
        Task<IEnumerable<SayisalLoto>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges);
        Task<SayisalLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(SayisalLoto sayisalLoto);
        void UpdateOneNumbersArray(SayisalLoto sayisalLoto);
        void DeleteOneNumbersArray(SayisalLoto sayisalLoto);
    }
}
