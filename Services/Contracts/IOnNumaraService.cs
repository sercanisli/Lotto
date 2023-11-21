using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IOnNumaraService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<OnNumaraParameters> linkParameters, bool trackChanges);
        Task<OnNumaraDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<OnNumaraDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges);
        Task<OnNumaraDto> CreateOneNumbersArrayAsync(OnNumaraDtoForInsertion onNumaraDtoForInsertion);
        Task UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task<List<int>> GetRondomNumbersAsync();
    }
}
