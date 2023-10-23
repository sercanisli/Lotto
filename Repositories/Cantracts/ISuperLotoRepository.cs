using Entities.Models;

namespace Repositories.Cantracts
{
    public interface ISuperLotoRepository : IRepositoryBase<SuperLoto>
    {
        IQueryable<SuperLoto> GetAllNumbersArray(bool trackChanges);
        IQueryable<SuperLoto> GetOneNumbersArrayById(int id, bool trackChanges);
        void CreateOneNumbersArray(SuperLoto superLoto);
        void UpdateOneNubersArray(SuperLoto superLoto);
        void DeleteOneNumbersArray(SuperLoto superLoto);
    }
}
