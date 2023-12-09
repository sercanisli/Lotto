using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface ISansTopuLogsService
    {
        Task<IEnumerable<SansTopuDtoForRandom>> GetAllLogsAsync();
        Task<SansTopuDtoForRandom> CreateLog(SansTopuDtoForRandom sansTopuDtoForRandom);
    }
}
