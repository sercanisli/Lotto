using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface ISuperLotoService
    {
        Task<(IEnumerable<ExpandoObject> superLotoDto, MetaData metaData)> GetAllNumbersArraysAsync(SuperLotoParameters superLotoParameters, bool trackChanges);
        Task<SuperLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SuperLotoDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges);
        Task<SuperLotoDto> CreateOneNumbersArrayAsync(SuperLotoDtoForInsertion superLotoDto);
        Task UpdateOneNumbersArrayAsync(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task<List<int>> GetRondomNumbersAsync();
    }
}
