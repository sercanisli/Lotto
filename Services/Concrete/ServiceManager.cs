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
        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repositoryManager, 
            ILoggerService logger, 
            IMapper mapper, 
            ISuperLotoLinks superLotoLinks, 
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _superLotoService = new Lazy<ISuperLotoService>(() => new SuperLotoManager(repositoryManager, logger, mapper, superLotoLinks));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger, mapper, userManager,configuration));
        }
        public ISuperLotoService SuperLotoService => _superLotoService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
