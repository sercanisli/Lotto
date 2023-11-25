using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface ISansTopuService
    {
        Task<IEnumerable<SansTopuDto>> GetAllNumbersArraysAsync(bool trackChanges);
        Task<SansTopuDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SansTopuDto> CreateOneNumbersArrayAsync(SansTopuDtoForInsertion sansTopuDtoForInsertion);
        Task UpdateOneNumbersArrayAsync(int id, SansTopuDtoForUpdate sansTopuDtoForUpdate, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
    }
}
