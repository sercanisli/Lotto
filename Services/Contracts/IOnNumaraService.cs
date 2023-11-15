using Entities.Models;

namespace Services.Contracts
{
    public interface IOnNumaraService
    {
        IEnumerable<OnNumara> GetAllNumbersArraysAsync(bool trackChanges);
        OnNumara GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        OnNumara CreateOneNumbersArrayAsync(OnNumara onNumara);
        void UpdateOneNumbersArrayAsync(int id, OnNumara onNumara);
        void DeleteOneNumbersArrayAsync(int id, bool trackChanges);
    }
}
