using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface ISansTopuService
    {
        Task<(IEnumerable<SansTopuDto> sansTopuDto, MetaData metaData)> GetAllNumbersArraysAsync(SansTopuParameters sansTopuParameters, bool trackChanges);
        Task<SansTopuDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SansTopuDto> CreateOneNumbersArrayAsync(SansTopuDtoForInsertion sansTopuDtoForInsertion);
        Task UpdateOneNumbersArrayAsync(int id, SansTopuDtoForUpdate sansTopuDtoForUpdate, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
    }
}
