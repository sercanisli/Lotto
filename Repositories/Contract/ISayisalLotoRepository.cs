using Entities.Models;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface ISayisalLotoRepository : IRepositoryBase<SayisalLoto>
    {
        IQueryable<SayisalLoto> GetAllNumbersArrayAsync(bool trackChanges);
        IQueryable<SayisalLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(SayisalLoto sayisalLoto);
        void UpdateOneNumbersArray(SayisalLoto sayisalLoto);
        void DeleteOneNumbersArray(SayisalLoto sayisalLoto);
    }
}
