using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IOnNumaraService
    {
        IEnumerable<OnNumaraDto> GetAllNumbersArraysAsync(bool trackChanges);
        OnNumaraDto GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        OnNumaraDto CreateOneNumbersArrayAsync(OnNumaraDtoForInsertion onNumaraDtoForInsertion);
        void UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges);
        void DeleteOneNumbersArrayAsync(int id, bool trackChanges);
    }
}
