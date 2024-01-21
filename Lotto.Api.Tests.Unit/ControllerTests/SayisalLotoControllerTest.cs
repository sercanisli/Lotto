using Entities.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Presentation.Controllers;
using Services.Contracts;

namespace Lotto.Api.Tests.Unit.ControllerTests
{
    public class SayisalLotoControllerTest
    {
        private readonly SayisalLotoController _sut;
        private readonly IServiceManager _serviceManager = Substitute.For<IServiceManager>();

        public SayisalLotoControllerTest()
        {
            _sut = new(_serviceManager);
        }

        [Fact]
        public async Task GetOneNumbersArrayByIdForSayisalLotoAsync_ShouldReturnOk()
        {
            //Arrange
            var sayisalLotoDto = new SayisalLotoDto()
            {
                Id = 9999,
                Numbers = { 5, 10, 15, 20, 25, 30 },
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };
            _serviceManager.SayisalLotoService.GetOneNumbersArrayByIdAsync(9999,false).Returns(sayisalLotoDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByIdForSayisalLotoAsync(9999);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneNumbersArrayByDateForSayisalLotoAsync_ShouldReturnOk()
        {
            //Arrange
            DateTime date = Convert.ToDateTime("27.02.1998 00:00:00");
            var sayisalLotoDto = new SayisalLotoDto()
            {
                Id = 9999,
                Numbers = { 5, 10, 15, 20, 25, 30 },
                Date = date
            };
            _serviceManager.SayisalLotoService.GetOneNumbersArrayByDateAsync(date, false).Returns(sayisalLotoDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByDateForSayisalLotoAsync(date);

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
