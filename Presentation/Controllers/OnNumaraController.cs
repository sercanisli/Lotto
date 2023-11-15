﻿using Entities.Models;
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
        public IActionResult GetAllNumbersArrayForOnNumaraAsync()
        {
            var entities = _manager.OnNumaraService.GetAllNumbersArraysAsync(false);
            return Ok(entities);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayByIdForOnNumaraAsync([FromRoute(Name = "id")] int id)
        {
            var entity = _manager.OnNumaraService.GetOneNumbersArrayByIdAsync(id, false);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult CreateOneNumbersArrayForOnNumaraAsync([FromBody] OnNumara onNumara)
        {
            if (onNumara == null)
            {
                return BadRequest();
            }
            _manager.OnNumaraService.CreateOneNumbersArrayAsync(onNumara);
            return StatusCode(201, onNumara);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArrayForOnNumaraAsync([FromRoute(Name = "id")] int id, [FromBody] OnNumara onNumara)
        {
            if (onNumara == null)
            {
                return BadRequest();
            }
            _manager.OnNumaraService.UpdateOneNumbersArrayAsync(id, onNumara, true);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArrayForOnNumaraAsync([FromRoute] int id)
        {
            _manager.OnNumaraService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }
    }
}
