using Entities.LogModels;

namespace Repositories.Contract
{
    public interface IOnNumaraLogsRepository
    {
        Task<IEnumerable<OnNumaraLogs>> GetAllLogsAsync(bool trackChanges);
        void CreateLog(OnNumaraLogs onNumaraLogs);
    }
}
