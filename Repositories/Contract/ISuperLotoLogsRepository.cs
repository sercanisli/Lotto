using Entities.LogModels;

namespace Repositories.Contract
{
    public interface ISuperLotoLogsRepository
    {
        Task<IEnumerable<SuperLotoLogs>> GetAllLogsAsync(bool trackChanges);
        void CreateLog(SuperLotoLogs superLotoLogs);
    }
}
