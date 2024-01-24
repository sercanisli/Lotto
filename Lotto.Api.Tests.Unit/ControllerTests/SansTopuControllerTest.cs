using Entities.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Presentation.Controllers;
using Services.Contracts;

namespace Lotto.Api.Tests.Unit.ControllerTests
{
    public class SansTopuControllerTest
    {
        private readonly SansTopuController _sut;
        private readonly IServiceManager _serviceManager = Substitute.For<IServiceManager>();

        public SansTopuControllerTest()
        {
            _sut = new(_serviceManager);
        }

        [Fact]
        public async Task GetOneNumbersArrayByIdForSansTopuAsync_ShouldReturnOk()
        {
            //Arrange
            int id = Arg.Any<int>();

            var sansTopuDto = new SansTopuDto()
            {
                Id = id,
                Numbers = { 5, 10, 15, 20, 25 },
                PlusNumber = 1,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };

            _serviceManager.SansTopuService.GetOneNumbersArrayByIdAsync(id, false).Returns(sansTopuDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByIdForSansTopuAsync(id);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneNumbersArrayByDateForSansTopuAsync_ShouldReturnOk()
        {
            //Arrange
            DateTime date = Arg.Any<DateTime>();
            var sansTopuDto = new SansTopuDto()
            {
                Id = 9999,
                Numbers = { 5, 10, 15, 20, 25 },
                PlusNumber = 1,
                Date = date
            };
            _serviceManager.SansTopuService.GetOneNumbersArrayByDateAsync(date, false).Returns(sansTopuDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByDateForSansTopuAsync(date);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        
    }
}
