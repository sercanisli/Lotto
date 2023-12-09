using Entities.DataTransferObjects;

namespace Repositories.Contract
{
    public interface ILogsRepositoryBase<T>
    {
        public Task<T> GetAllLogs();
        public Task<T> CreateLog(T entity);
    }
}
