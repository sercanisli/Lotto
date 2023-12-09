using Entities.DataTransferObjects;

namespace Repositories.Contract
{
    public interface ILogsRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        void Create(T entity);
    }
}
