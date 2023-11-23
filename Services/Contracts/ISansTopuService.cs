using Entities.Models;

namespace Services.Contracts
{
    public interface ISansTopuService
    {
        IEnumerable<SansTopu> GetAllNumbersArrays(bool trackChanges);
        SansTopu GetOneNumbersArrayById(int id, bool trackChanges);
        SansTopu CreateOneNumbersArray(SansTopu sansTopu);
        void UpdateOneNumbersArray(int id, SansTopu sansTopu, bool trackChanges);
        void DeleteOneNumbersArray(int id, bool trackChanges;
    }
}
