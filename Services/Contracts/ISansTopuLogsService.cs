using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface ISansTopuLogsService
    {
        Task<IEnumerable<SansTopuDtoForRandom>> GetAllLogsAsync(bool trackChanges);
        Task<SansTopuDtoForRandom> CreateLog(SansTopuDtoForRandom sansTopuDtoForRandom);
    }
}
