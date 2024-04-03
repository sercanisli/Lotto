using Entities.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Presentation.Controllers;
using Services.Contracts;

namespace Lotto.Api.Tests.Unit.ControllerTests
{
    public class OnNumaraControllerTest
    {
        private readonly OnNumaraController _sut;
        private readonly IServiceManager _serviceManager = Substitute.For<IServiceManager>();
        private readonly HttpContext _httpContext = Substitute.For<HttpContext>();

        List<int> numbers = new List<int> { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44 };

        public OnNumaraControllerTest()
        {
            _sut = new(_serviceManager);
        }

        [Fact]
        public async Task GetOneNumbersArrayByIdForOnNumaraAsync_ShouldReturnOk()
        {
            //Arrange
            int id = Arg.Any<int>();

            var onNumaraDto = new OnNumaraDto()
            {
                Id = id,
                Numbers = numbers,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };

            _serviceManager.OnNumaraService.GetOneNumbersArrayByIdAsync(id, false).Returns(onNumaraDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByIdForOnNumaraAsync(id);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneNumbersArrayByDateForOnNumaraAsync_ShouldReturnOk()
        {
            //Arrange
            DateTime date = Arg.Any<DateTime>();

            var onNumaraDto = new OnNumaraDto()
            {
                Id = 9999,
                Numbers = numbers,
                Date = date
            };

            _serviceManager.OnNumaraService.GetOneNumbersArrayByDateAsync(date, false).Returns(onNumaraDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByDateForOnNumaraAsync(date);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CreateOneNumbersArrayForOnNumaraAsync_ShouldReturnCreated()
        {
            //Arrange
            var onNumaraDtoForInsertion = new OnNumaraDtoForInsertion()
            {
                Numbers = numbers,
                Date = "27.02.1998 00:00:00"
            };

            var onNumaraDto = new OnNumaraDto()
            {
                Id = 9999,
                Numbers = numbers,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };

            _serviceManager.OnNumaraService.CreateOneNumbersArrayAsync(onNumaraDtoForInsertion).Returns(onNumaraDto);

            //Act
            var result = (ObjectResult)await _sut.CreateOneNumbersArrayForOnNumaraAsync(onNumaraDtoForInsertion);

            //Assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateOneNumbersArrayForOnNumaraAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();

            var onNumaraDtoForUpdate = new OnNumaraDtoForUpdate()
            {
                Id = id,
                Numbers = numbers,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };

            _serviceManager.OnNumaraService.UpdateOneNumbersArrayAsync(id, onNumaraDtoForUpdate, false);

            //Act
            var result = (NoContentResult)await _sut.UpdateOneNumbersArrayForOnNumaraAsync(id, onNumaraDtoForUpdate);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteOneNumbersArrayForOnNumaraAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();

            _serviceManager.OnNumaraService.DeleteOneNumbersArrayAsync(id, false);

            //Act
            var result = (NoContentResult)await _sut.DeleteOneNumbersArrayForOnNumaraAsync(id);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task CompareReleasedOnNumaraNumbersWithAllOnNumaraNumbersAsync_ShouldReturnOk()
        {
            //Arrange
            _serviceManager.OnNumaraService.CompareOnNumaraNumbersAsync(Arg.Any<OnNumaraDtoForCompare>());

            //Act
            var result = (OkObjectResult)await _sut.CompareReleasedOnNumaraNumbersWithAllOnNumaraNumbersAsync(Arg.Any<OnNumaraDtoForCompare>());

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync_ShouldReturnOk()
        {
            //Arrange
            _serviceManager.OnNumaraService.CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync(Arg.Any<OnNumaraDtoForCompareWithLogs>());

            //Act
            var result = (OkObjectResult)await _sut.CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync(Arg.Any<OnNumaraDtoForCompareWithLogs>());

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOnNumaraLastItemAsync_ShouldReturnOk()
        {
            //Arrange
            var onNumaraDtoForLastItem = new OnNumaraDtoForLastItem()
            {
                Id = 9999,
                Numbers = numbers,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };
            _serviceManager.OnNumaraService.GetLastItemAsync(false).Returns(onNumaraDtoForLastItem);

            //Act
            var result = (OkObjectResult)await _sut.GetOnNumaraLastItemAsync();

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
