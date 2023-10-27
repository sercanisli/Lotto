using AutoMapper;
using Entities.DataTransferObjects;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ISuperLotoService> _superLotoService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper, IDataShaper<SuperLotoDto> shaper)
        {
            _superLotoService = new Lazy<ISuperLotoService>(() => new SuperLotoManager(repositoryManager, logger, mapper, shaper));
        }
        public ISuperLotoService SuperLotoService => _superLotoService.Value;
    }
}
