using Entities.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Presentation.Controllers;
using Services.Contracts;

namespace Lotto.Api.Tests.Unit.ControllerTests
{
    public class SuperLotoControllerTest
    {
        private readonly SuperLotoController _sut;
        private readonly IServiceManager _serviceManager = Substitute.For<IServiceManager>();

        public SuperLotoControllerTest()
        {
            _sut = new(_serviceManager);
        }

        [Fact]
        public async Task GetOneNumbersArrayByIdForSuperLotoAsync_ShouldReturnOk()
        {
            int id = Arg.Any<int>();
            //Arrange
            var superLotoDto = new SuperLotoDto()
            {
                Id = id,
                Numbers = { 5, 10, 15, 20, 25, 30 },
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };

            _serviceManager.SuperLotoService.GetOneNumbersArrayByIdAsync(id, false).Returns(superLotoDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByIdForSuperLotoAsync(id);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneNumbersArrayByDateForSuperLotoAsync_ShouldReturnOk()
        {
            //Arrange
            DateTime date = Arg.Any<DateTime>();
            var superLotoDto = new SuperLotoDto()
            {
                Id = 9999,
                Numbers = { 5, 10, 15, 20, 25, 30 },
                Date = date
            };
            _serviceManager.SuperLotoService.GetOneNumbersArrayByDateAsync(date, false).Returns(superLotoDto);

            //Act
            var result = (OkObjectResult)await _sut.GetOneNumbersArrayByDateForSuperLotoAsync(date);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CreateOneNumbersArrayForSuperLotoAsync_ShouldReturnOk()
        {
            //Arrange
            var numbers = new List<int> { 5, 10, 15, 20, 25, 30 };

            var superLotoDtoForInsertion = new SuperLotoDtoForInsertion()
            {
                Numbers = numbers,
                Date = "27.02.1998 00:00:00"
            };

            var superLotoDto = new SuperLotoDto()
            {
                Id = 9999,
                Numbers = numbers,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };

            _serviceManager.SuperLotoService.CreateOneNumbersArrayAsync(superLotoDtoForInsertion).Returns(superLotoDto);

            //Act
            var result = (ObjectResult)await _sut.CreateOneNumbersArrayForSuperLotoAsync(superLotoDtoForInsertion);

            //Assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateOneNumbersArrayForSuperLotoAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();
            var numbers = new List<int> { 5, 10, 15, 20, 25, 30 };

            var superLotoDtoForUpdate = new SuperLotoDtoForUpdate()
            {
                Id = id,
                Numbers = numbers,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };
            _serviceManager.SuperLotoService.UpdateOneNumbersArrayAsync(id, superLotoDtoForUpdate, false);

            //Act
            var result = (NoContentResult)await _sut.UpdateOneNumbersArrayForSuperLotoAsync(id, superLotoDtoForUpdate);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteOneNumbersArrayForSuperLotoAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();

            _serviceManager.SuperLotoService.DeleteOneNumbersArrayAsync(id, false);

            //Act
            var result = (NoContentResult)await _sut.DeleteOneNumbersArrayForSuperLotoAsync(id);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task CompareRelasedSuperLotoNumbersWithAllSuperLotoNumbersAsync_ShouldReturnOk()
        {
            //Arrange
            _serviceManager.SuperLotoService.CompareSuperLotoNumbersAsync(Arg.Any<SuperLotoDtoForCompare>());

            //Act
            var result = (OkObjectResult)await _sut.CompareRelasedSuperLotoNumbersWithAllSuperLotoNumbersAsync(Arg.Any<SuperLotoDtoForCompare>());

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
