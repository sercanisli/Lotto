using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Repositories.Cantracts;
using Repositories.EntityFrameworkCore;
using Services.Concrete;
using Services.Contracts;

namespace Lotto.Api.Tests.Unit.ServiceTests
{
    public class SansTopuServiceTests
    {
        private readonly SansTopuManager _sut;
        private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
        private readonly ILoggerService _loggerService = Substitute.For<ILoggerService>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly ISansTopuLinks _links = Substitute.For<ISansTopuLinks>();
        private readonly UserManager<User> _userManager;
        private readonly ICacheService _cache = Substitute.For<ICacheService>();

        public SansTopuServiceTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

            var dbContext = new RepositoryContext(dbContextOptions);

            var serviceProvider = new ServiceCollection().AddIdentity<User, IdentityRole>().Services.BuildServiceProvider();
            var userStore = new UserStore<User>(dbContext, null);

            _userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);

            _sut = new SansTopuManager(_repositoryManager, _loggerService, _mapper, _links, _userManager, _cache);
        }

        [Fact]
        public async Task GetOneNumbersArrayByIdAsync_ShouldReturnSansTopuDto()
        {
           
        }
    }
}
