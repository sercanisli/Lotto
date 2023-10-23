using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ISuperLotoService> _superLotoService;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _superLotoService = new Lazy<ISuperLotoService>(() => new SuperLotoManager(repositoryManager));
        }
        public ISuperLotoService SuperLoto => _superLotoService.Value;
    }
}
