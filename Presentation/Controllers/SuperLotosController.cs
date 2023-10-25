﻿using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/superloto")]
    public class SuperLotoController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public SuperLotoController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNumbersArrayAsync()
        {
            var numbers = await _manager.SuperLotoService.GetAllNumbersArraysAsync(false);
            return Ok(numbers);
        }

        [HttpGet("GetOnlyNumbersAsync")]
        public async Task<IActionResult> GetOnlyNumbersAsync()
        {
            var numbers = await _manager.SuperLotoService.GetOnlyNumbersAsync(false);
            return Ok(numbers);
        }

        [HttpGet("GetRandomNumbersAsync")]
        public async Task<IActionResult> GetRandomNumbersAsync()
        {
            var numbers = await _manager.SuperLotoService.GetRondomNumbersAsync();
            return Ok(numbers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneNumbersArrayByIdAsync([FromRoute(Name = "id")] int id)
        {
            var array = await _manager.SuperLotoService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(array);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneNumbersArrayAsync([FromBody] SuperLotoDtoForInsertion superLotoDto)
        {
            if (superLotoDto == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var entity = await _manager.SuperLotoService.CreateOneNumbersArrayAsync(superLotoDto);
            return StatusCode(201, entity);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneNumbersArrayAsync([FromRoute(Name = "id")] int id, [FromBody] SuperLotoDtoForUpdate superLotoDto)
        {
            if (superLotoDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _manager.SuperLotoService.UpdateOneNumbersArrayAsync(id, superLotoDto, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneNumbersArrayAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.SuperLotoService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }
    }
}
