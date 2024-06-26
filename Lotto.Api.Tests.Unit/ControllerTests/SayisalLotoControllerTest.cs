﻿using Entities.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
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

        [Fact]
        public async Task CreateOneNumbersArrayForSayisalLotoAsync_ShouldReturnCreated()
        {
            //Arrange
            var numbers = new List<int> { 5, 10, 15, 20, 25, 30 };

            var sayisalLotoDtoForInsertion = new SayisalLotoDtoForInsertion
            {
                Numbers = numbers,
                Date = "27.02.1998 00:00:00"
            };

            var sayisalLotoDto = new SayisalLotoDto
            {
                Id = 9999,
                Numbers = numbers,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };

            _serviceManager.SayisalLotoService.CreateOneNumbersArrayAsync(sayisalLotoDtoForInsertion).Returns(sayisalLotoDto);

            //Act
            var result = (ObjectResult)await _sut.CreateOneNumbersArrayForSayisalLotoAsync(sayisalLotoDtoForInsertion);

            //Assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateOneNumbersArrayForSayisalLotoAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();
            var numbers = new List<int> { 5, 10, 15, 20, 25, 30 };

            var sayisalLotoDtoForUpdate = new SayisalLotoDtoForUpdate()
            {
                Id = id,
                Numbers = numbers,
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };

            _serviceManager.SayisalLotoService.UpdateOneNumbersArrayAsync(id, sayisalLotoDtoForUpdate, false);

            //Act
            var result = (NoContentResult)await _sut.UpdateOneNumbersArrayForSayisalLotoAsync(id, sayisalLotoDtoForUpdate);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteOneNumbersArrayForSayisalLotoAsync_ShouldReturnNoContent()
        {
            //Arrange
            int id = Arg.Any<int>();

            _serviceManager.SayisalLotoService.DeleteOneNumbersArrayAsync(id, false);

            //Act
            var result = (NoContentResult)await _sut.DeleteOneNumbersArrayForSayisalLotoAsync(id);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task CompareReleasedSayisalLotoNumbersWithAllSayisalLotoNumbersAsync_ShouldReturnOk()
        {
            //Arrange
            _serviceManager.SayisalLotoService.CompareSayisalLotoNumbersAsync(Arg.Any<SayisalLotoDtoForCompare>());

            //Act
            var result = (OkObjectResult)await _sut.CompareReleasedSayisalLotoNumbersWithAllSayisalLotoNumbersAsync(Arg.Any<SayisalLotoDtoForCompare>());

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync_ShouldReturnOk()
        {
            //Arrange
            _serviceManager.SayisalLotoService.CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync(Arg.Any<SayisalLotoDtoForCompareWithLogs>());

            //Act
            var result = (OkObjectResult)await _sut.CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync(Arg.Any<SayisalLotoDtoForCompareWithLogs>());

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetSayisalLotoLastItemAsync_ShouldReturnOk()
        {
            //Arrange
            var sayisalLotoDtoForLastItem = new SayisalLotoDtoForLastItem()
            {
                Id = 9999,
                Numbers = new List<int> { 5, 10, 15, 20, 25, 30 },
                Date = Convert.ToDateTime("27.02.1998 00:00:00")
            };
            _serviceManager.SayisalLotoService.GetLastItemAsync(false).Returns(sayisalLotoDtoForLastItem);

            //Act
            var result = (OkObjectResult)await _sut.GetSayisalLotoLastItemAsync();

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
