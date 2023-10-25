namespace Repositories.Cantracts
{
    public interface IRepositoryManager
    {
        ISuperLotoRepository SuperLoto { get; }

        Task SaveAsync();
    }
}
