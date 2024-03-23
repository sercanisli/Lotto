﻿using Entities.DataTransferObjects;
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
    }
}
