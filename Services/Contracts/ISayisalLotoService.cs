using Entities.Models;

namespace Services.Contracts
{
    public interface ISayisalLotoService
    {
        IEnumerable<SayisalLoto> GetAllNumbersArraysAsync(bool trackChanges);
        SayisalLoto GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        SayisalLoto CreateOneNumbersArrayAsync(SayisalLoto sayisalLoto);
        void DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        void UpdateOneNumbersArrayAsync(int id, SayisalLoto sayisalLoto, bool trackChanges);
    }
}
