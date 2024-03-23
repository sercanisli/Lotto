using Entities.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Presentation.Controllers;
using Services.Contracts;

namespace Lotto.Api.Tests.Unit.ControllerTests
{
    public class WinningNumbersControllerTest
    {
        private readonly WinningNumbersController _sut;
        private readonly IServiceManager _serviceManager = Substitute.For<IServiceManager>();   
        private readonly HttpContext _httpContext = Substitute.For<HttpContext>();

        public WinningNumbersControllerTest()
        {
            _sut = new(_serviceManager);
        }

        [Fact]
        public async Task GetOneWinningNumbersForWinningNumbersAsync_ShouldReturnOk()
        {
            //Arrange
            int id = Arg.Any<int>();
            var winningNumbersDto = new WinnigNumbersDto()
            {
                Id = id,
                Title = "Title",
                Description = "Description"
            };

            _serviceManager.WinningNumbersService.GetOneWinningNumbersAsync(id, false).Returns(winningNumbersDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneWinningNumbersAsync(id);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CreateOneWinningNumbersAsync_ShouldReturnCreated()
        {
            //Arrange
            var winningNumbersDto = new WinnigNumbersDto()
            {
                Id = 1,
                Title = "Title",
                Description = "Description"
            };

            _serviceManager.WinningNumbersService.CreateOneWinningNumbersAsync(winningNumbersDto).Returns(winningNumbersDto);

            //Act
            var result = (ObjectResult)await _sut.CreateOneWinningNumbersAsync(winningNumbersDto);

            //Assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateOneWinningNumbersAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();
            var winningNumbersDto = new WinnigNumbersDto()
            {
                Id = id,
                Title = "Title",
                Description = "Description"
            };

            _serviceManager.WinningNumbersService.UpdateWinningNumbersAsync(id, winningNumbersDto, false);

            //Act
            var result = (NoContentResult)await _sut.UpdateOneWinningNumbersAsync(id, winningNumbersDto);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteOneWinningNumbersAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();

            //Act
            var result = (NoContentResult)await _sut.DeleteOneWinningNumbersAsync(id);

            //Assert
            result.StatusCode.Should().Be(204);
        }
    }
}
