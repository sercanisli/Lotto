using Entities.LogModels;

namespace Repositories.Contract
{
    public interface ISansTopuLogsRepository : ILogsRepositoryBase<SansTopuLogs>
    {
        Task<IEnumerable<SansTopuLogs>> GetAllLogsAsync(bool trackChanges);
        void CreateLog(SansTopuLogs sansTopuLogs);

    }
}
