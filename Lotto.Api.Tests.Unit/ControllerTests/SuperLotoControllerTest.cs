﻿using Entities.DataTransferObjects;
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
    }
}