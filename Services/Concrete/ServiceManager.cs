using AutoMapper;
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

        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repositoryManager, 
            ILoggerService logger, 
            IMapper mapper, 
            ISuperLotoLinks superLotoLinks, 
            ISayisalLotoLinks sayisalLotoLinks,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _superLotoService = new Lazy<ISuperLotoService>(() => new SuperLotoManager(repositoryManager, logger, mapper, superLotoLinks));
            _sayisalLotoService = new Lazy<ISayisalLotoService>(() => new SayisalLotoManager(repositoryManager, logger, mapper, sayisalLotoLinks));
            _onNumaraService = new Lazy<IOnNumaraService>(() => new OnNumaraManager(repositoryManager));

            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger, mapper, userManager,configuration));
        }

        public ISuperLotoService SuperLotoService => _superLotoService.Value;
        public ISayisalLotoService SayisalLotoService => _sayisalLotoService.Value;
        public IOnNumaraService OnNumaraService => _onNumaraService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

    }
}
