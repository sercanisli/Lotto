using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface ISayisalLotoService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<SayisalLotoParameters> linkParameters, bool trackChanges);
        Task<SayisalLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SayisalLotoDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges);
        Task<SayisalLotoDto> CreateOneNumbersArrayAsync(SayisalLotoDtoForInsertion sayisalLotoDtoForInsertion);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task UpdateOneNumbersArrayAsync(int id, SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate, bool trackChanges);
        Task<MatchRateDto> CompareSayisalLotoNumbersAsync(SayisalLotoDtoForCompare sayisalLotoDtoForCompare );
        Task<MatchRateDto> CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync(SayisalLotoDtoForCompareWithLogs lotoDtoForCompareWithLogs);
        Task<SayisalLotoDtoForRandom> GetRondomNumbersAsync(string userName);
    }
}
