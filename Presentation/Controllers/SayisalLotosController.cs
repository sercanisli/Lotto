using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/sayisalloto")]
    public class SayisalLotosController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public SayisalLotosController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllNumbersArrayForSayisalLotoAsync")]
        public IActionResult GetAllNumbersArrayForSayisalLotoAsync()
        {
            var entities = _manager.SayisalLotoService.GetAllNumbersArraysAsync(false);
            return Ok(entities);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayByIdForSayisalLotoAsync([FromRoute(Name = "id")] int id)
        {
            var entity = _manager.SayisalLotoService.GetOneNumbersArrayByIdAsync(id, false);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult CreateOneNumbersArrayForSayisalLotoAsync([FromBody] SayisalLoto sayisalLoto)
        {
            if (sayisalLoto == null)
            {
                return BadRequest();
            }
            _manager.SayisalLotoService.CreateOneNumbersArrayAsync(sayisalLoto);
            return StatusCode(201, sayisalLoto);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArrayForSayisalLotoAsync([FromRoute(Name = "id")] int id, [FromBody] SayisalLoto sayisalLoto)
        {
            if (sayisalLoto == null)
            {
                return BadRequest();
            }

            _manager.SayisalLotoService.UpdateOneNumbersArrayAsync(id, sayisalLoto, true);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArrayForSayisalLotoAsync([FromRoute(Name = "id")] int id)
        {
            _manager.SayisalLotoService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }
    }
}
