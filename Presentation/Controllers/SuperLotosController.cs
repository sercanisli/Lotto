using Entities.DataTransferObjects;
using Entities.Models;
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
        public IActionResult GetAllNumbersArray()
        {
            var numbers = _manager.SuperLotoService.GetAllNumbersArrays(false);
            return Ok(numbers);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayById([FromRoute(Name = "id")] int id)
        {
            var array = _manager.SuperLotoService.GetOneNumbersArrayById(id, false);
            return Ok(array);
        }

        [HttpPost]
        public IActionResult CreateOneNumbersArray([FromBody] SuperLotoDtoForInsertion superLotoDto)
        {
            if (superLotoDto == null)
            {
                return BadRequest();
            }
            var entity = _manager.SuperLotoService.CreateOneNumbersArray(superLotoDto);
            return StatusCode(201, entity);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArray([FromRoute(Name = "id")] int id, [FromBody] SuperLotoDtoForUpdate superLotoDto)
        {
            if (superLotoDto == null)
            {
                return BadRequest();
            }
            _manager.SuperLotoService.UpdateOneNumbersArray(id, superLotoDto, true);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArray([FromRoute(Name = "id")] int id)
        {
            _manager.SuperLotoService.DeleteOneNumbersArray(id, false);
            return NoContent();
        }
    }
}
