using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
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
            ICacheService cache
            )
        {
            _superLotoService = new Lazy<ISuperLotoService>(() => new SuperLotoManager(repositoryManager, logger, mapper, superLotoLinks, userManager));
            _sayisalLotoService = new Lazy<ISayisalLotoService>(() => new SayisalLotoManager(repositoryManager, logger, mapper, sayisalLotoLinks, userManager, cache));
            _onNumaraService = new Lazy<IOnNumaraService>(() => new OnNumaraManager(repositoryManager, logger, mapper, onNumaraLinks, userManager, cache));
            _sansTopuService = new Lazy<ISansTopuService>(() => new SansTopuManager(repositoryManager, logger, mapper, sansTopuLinks, userManager, cache));

            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger, mapper, userManager,configuration));
        }

        public ISuperLotoService SuperLotoService => _superLotoService.Value;
        public ISayisalLotoService SayisalLotoService => _sayisalLotoService.Value;
        public IOnNumaraService OnNumaraService => _onNumaraService.Value;
        public ISansTopuService SansTopuService => _sansTopuService.Value;


        public IAuthenticationService AuthenticationService => _authenticationService.Value;

    }
}
