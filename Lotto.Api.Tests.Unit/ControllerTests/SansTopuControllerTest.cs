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

        [Fact]
        public async Task CreateOneNumbersArrayForSansTopuAsync_ShouldReturnCreated()
        {
            //Arrange
            var numbers = new List<int> { 5, 10, 15, 20, 25, };
            int plusNumber = 1;

            var sansTopuDtoForInsertion = new SansTopuDtoForInsertion()
            {
                Numbers = numbers,
                PlusNumber = plusNumber,
                Date = "27.02.1998 00:00:00"
            };

            var sansTopuDto = new SansTopuDto()
            {
                Id = 9999,
                Numbers = numbers,
                PlusNumber = plusNumber,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };
            _serviceManager.SansTopuService.CreateOneNumbersArrayAsync(sansTopuDtoForInsertion).Returns(sansTopuDto);

            //Act
            var result = (ObjectResult)await _sut.CreateOneNumbersArrayForSansTopuAsync(sansTopuDtoForInsertion);

            //Assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateOneNumbersArrayForSansTopuAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();
            var numbers = new List<int> { 5, 10, 15, 20, 25 };
            int plusNumber = 1;

            var sansTopuDtoForUpdate = new SansTopuDtoForUpdate()
            {
                Id = id,
                Numbers = numbers,
                PlusNumber = plusNumber,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };
            _serviceManager.SansTopuService.UpdateOneNumbersArrayAsync(id, sansTopuDtoForUpdate, false);

            //Act
            var result = (NoContentResult)await _sut.UpdateOneNumbersArrayForSansTopuAsync(id, sansTopuDtoForUpdate);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteOneNumbersArrayForSansTopuAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();

            _serviceManager.SansTopuService.DeleteOneNumbersArrayAsync(id, false);

            //Act
            var result = (NoContentResult)await _sut.DeleteOneNumbersArrayForSansTopuAsync(id);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task CompareReleasedSansTopuNumbersWithAllSansTopuNumbersAsync_ShouldReturnOk()
        {
            //Arrange
            _serviceManager.SansTopuService.CompareSansTopuNumbersAsync(Arg.Any<SansTopuDtoForCompare>());

            //Act
            var result = (OkObjectResult)await _sut.CompareReleasedSansTopuNumbersWithAllSansTopuNumbersAsync(Arg.Any<SansTopuDtoForCompare>());

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CompareSansTopuNumbersWithSansTopuLogsNumbersAsync_ShouldReturnOk()
        {
            //Arrange
            _serviceManager.SansTopuService.CompareSansTopuNumbersWithSansTopuLogsNumbersAsync(Arg.Any<SansTopuDtoForCompareWithLogs>());

            //Act
            var result = (OkObjectResult)await _sut.CompareSansTopuNumbersWithSansTopuLogsNumbersAsync(Arg.Any<SansTopuDtoForCompareWithLogs>());

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
