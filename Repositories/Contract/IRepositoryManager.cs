using Repositories.Contract;

namespace Repositories.Cantracts
{
    public interface IRepositoryManager
    {
        ISuperLotoRepository SuperLoto { get; }
        ISayisalLotoRepository SayisalLoto { get; }
        IOnNumaraRepository OnNumara { get; }
        ISansTopuRepository SansTopu { get; }


        ISansTopuGetRandomLogsRepository SansTopuGetRandomLogs { get; }

        Task SaveAsync();
    }
}
