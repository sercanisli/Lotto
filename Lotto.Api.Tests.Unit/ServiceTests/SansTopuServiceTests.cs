using AutoMapper;
using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Repositories.Cantracts;
using Services.Concrete;
using Services.Contracts;

namespace Lotto.Api.Tests.Unit.ServiceTests
{
    public class SansTopuServiceTests
    {
        private readonly SansTopuManager _sut;
        private readonly IRepositoryManager _manager = Substitute.For<IRepositoryManager>();
        private readonly ILoggerService _loggerService = Substitute.For<ILoggerService>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly ISansTopuLinks _links = Substitute.For<ISansTopuLinks>();
        private readonly UserManager<User> _userManager = Substitute.For<UserManager<User>>();
        private readonly ICacheService _cache = Substitute.For<ICacheService>();

        public SansTopuServiceTests()
        {
            _sut = new(_manager, _loggerService, _mapper, _links, _userManager, _cache);
        }

        [Fact]
        public async Task GetOneNumbersArrayByIdAsync_ShouldReturnNull_WhenNoUserExist()
        {
            // Arrange
            _manager.SansTopu.GetOneNumbersArrayByIdAsync(Arg.Any<int>(), false).ReturnsNull();

            //Act
            var result = await _sut.GetOneNumbersArrayByIdAsync(Arg.Any<int>(), false);

            //Assert
            result.Should().BeNull();
        }
    }
}
