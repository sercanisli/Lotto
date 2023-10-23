using Entities.Models;

namespace Services.Contracts
{
    public interface ISuperLotoService
    {
        IEnumerable<SuperLoto> GetAllNumbersArrays(bool trackChanges);
        SuperLoto GetOneNumbersArrayById(int id, bool trackChanges);
        SuperLoto CreateOneNumbersArray(SuperLoto superLoto);
        void UpdateOneNumbersArray(int id, SuperLoto superLoto, bool trackChanges);
        void DeleteOneNumbersArray(int id, bool trackChanges);

    }
}
