using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface ISansTopuService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<SansTopuParameters> linkParameters, bool trackChanges);
        Task<SansTopuDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SansTopuDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges);
        Task<SansTopuDto> CreateOneNumbersArrayAsync(SansTopuDtoForInsertion sansTopuDtoForInsertion);
        Task UpdateOneNumbersArrayAsync(int id, SansTopuDtoForUpdate sansTopuDtoForUpdate, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
    }
}
