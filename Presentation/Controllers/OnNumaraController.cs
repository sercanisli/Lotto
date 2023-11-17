﻿using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/onnumara")]
    public class OnNumaraController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public OnNumaraController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllNumbersArrayForOnNumaraAsync")]
        public async Task<IActionResult> GetAllNumbersArrayForOnNumaraAsync()
        {
            var entities = await _manager.OnNumaraService.GetAllNumbersArraysAsync(false);
            return Ok(entities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneNumbersArrayByIdForOnNumaraAsync([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.OnNumaraService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneNumbersArrayForOnNumaraAsync([FromBody] OnNumaraDtoForInsertion onNumaraDtoForInsertion)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var entity = await _manager.OnNumaraService.CreateOneNumbersArrayAsync(onNumaraDtoForInsertion);
            return StatusCode(201, entity);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneNumbersArrayForOnNumaraAsync([FromRoute(Name = "id")] int id, [FromBody] OnNumaraDtoForUpdate onNumaraDtoForUpdate)
        {
            if (onNumaraDtoForUpdate == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _manager.OnNumaraService.UpdateOneNumbersArrayAsync(id, onNumaraDtoForUpdate, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneNumbersArrayForOnNumaraAsync([FromRoute] int id)
        {
            await _manager.OnNumaraService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }
    }
}
