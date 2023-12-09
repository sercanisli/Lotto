using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Cantracts;
using Services.Contracts;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ISuperLotoService> _superLotoService;
        private readonly Lazy<ISayisalLotoService> _sayisalLotoService;
        private readonly Lazy<IOnNumaraService> _onNumaraService;
        private readonly Lazy<ISansTopuService> _sansTopuService;

        private readonly Lazy<ISansTopuLogsService> _sansTopuLogsService;

        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repositoryManager, 
            ILoggerService logger, 
            IMapper mapper, 
            ISuperLotoLinks superLotoLinks, 
            ISayisalLotoLinks sayisalLotoLinks,
            IOnNumaraLinks onNumaraLinks,
            ISansTopuLinks sansTopuLinks,
            UserManager<User> userManager,
            IConfiguration configuration,
            ISansTopuLogsService sansTopuLogsService
            )
        {
            _superLotoService = new Lazy<ISuperLotoService>(() => new SuperLotoManager(repositoryManager, logger, mapper, superLotoLinks));
            _sayisalLotoService = new Lazy<ISayisalLotoService>(() => new SayisalLotoManager(repositoryManager, logger, mapper, sayisalLotoLinks));
            _onNumaraService = new Lazy<IOnNumaraService>(() => new OnNumaraManager(repositoryManager, logger, mapper, onNumaraLinks, userManager));
            _sansTopuService = new Lazy<ISansTopuService>(() => new SansTopuManager(repositoryManager, logger, mapper, sansTopuLinks, userManager, sansTopuLogsService));

            _sansTopuLogsService = new Lazy<ISansTopuLogsService>(() => new SansTopuLogsManager(repositoryManager, mapper));

            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger, mapper, userManager,configuration));
        }

        public ISuperLotoService SuperLotoService => _superLotoService.Value;
        public ISayisalLotoService SayisalLotoService => _sayisalLotoService.Value;
        public IOnNumaraService OnNumaraService => _onNumaraService.Value;
        public ISansTopuService SansTopuService => _sansTopuService.Value;


        public ISansTopuLogsService SansTopuLogsService => _sansTopuLogsService.Value;


        public IAuthenticationService AuthenticationService => _authenticationService.Value;

    }
}
