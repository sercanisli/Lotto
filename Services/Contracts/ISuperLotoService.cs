using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface ISuperLotoService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<SuperLotoParameters> linkParameters, bool trackChanges);
        Task<SuperLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SuperLotoDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges);
        Task<SuperLotoDto> CreateOneNumbersArrayAsync(SuperLotoDtoForInsertion superLotoDto);
        Task UpdateOneNumbersArrayAsync(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task<MatchRateDto> CompareSuperLotoNumbersAsync(SuperLotoDtoForCompare superLotoDtoForCompare);
        Task<MatchRateDto> CompareSuperLotoNumbersWithSuperLotoLogsNumbersAsync(SuperLotoDtoForCompareWithLogs superLotoDtoForCompareWithLogs);
        Task<SuperLotoDtoForRandom> GetRondomNumbersAsync(string userName);
    }
}
