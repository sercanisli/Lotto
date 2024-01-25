using Entities.DataTransferObjects;
using FluentAssertions;
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
                Numbers = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44 },
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
                Numbers = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44 },
                Date = date
            };

            _serviceManager.OnNumaraService.GetOneNumbersArrayByDateAsync(date, false).Returns(onNumaraDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByDateForOnNumaraAsync(date);

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
