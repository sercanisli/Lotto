using Entities.LogModels;

namespace Repositories.Contract
{
    public interface ISayisalLotoLogsRepository
    {
        Task<IEnumerable<SayisalLotoLogs>> GetAllLogsAsync(bool trackChanges);
        void CreateLog(SayisalLotoLogs sayisalLotoLogs);
    }
}
