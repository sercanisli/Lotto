using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ISuperLotoService> _superLotoService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService logger)
        {
            _superLotoService = new Lazy<ISuperLotoService>(() => new SuperLotoManager(repositoryManager, logger));
        }
        public ISuperLotoService SuperLotoService => _superLotoService.Value;
    }
}
