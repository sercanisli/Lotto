using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IOnNumaraService
    {
        IEnumerable<OnNumaraDto> GetAllNumbersArraysAsync(bool trackChanges);
        OnNumara GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        OnNumara CreateOneNumbersArrayAsync(OnNumara onNumara);
        void UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges);
        void DeleteOneNumbersArrayAsync(int id, bool trackChanges);
    }
}
