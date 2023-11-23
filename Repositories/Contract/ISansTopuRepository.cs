using Entities.Models;
using Repositories.Cantracts;

namespace Repositories.Contract
{
    public interface ISansTopuRepository : IRepositoryBase<SansTopu>
    {
        IQueryable<SansTopu> GetAllNumbersArray(bool trackChanges);
        SansTopu GetOneNumbersArrayById(bool trackChanges);
        void CreateOneNumbersArray(SansTopu topu);
        void UpdateOneNumbersArray(SansTopu topu);
        void DeleteOneNumbersArray(SansTopu topu);
    }
}
