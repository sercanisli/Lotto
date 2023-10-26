using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface ISuperLotoService
    {
        Task<IEnumerable<SuperLotoDto>> GetAllNumbersArraysAsync(SuperLotoParameters superLotoParameters, bool trackChanges);
        Task<SuperLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SuperLotoDto> CreateOneNumbersArrayAsync(SuperLotoDtoForInsertion superLotoDto);
        Task UpdateOneNumbersArrayAsync(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task<List<int>> GetRondomNumbersAsync();
    }
}
