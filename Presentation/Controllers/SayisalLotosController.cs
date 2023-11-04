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

        [HttpGet(Name = "GetAllNumbersArrayAsync")]
        public IActionResult GetAllNumbersArrayAsync() 
        {
            try
            {
                var entities = _manager.SayisalLotoService.GetAllNumbersArraysAsync(false);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayByIdAsync([FromRoute(Name = "id")]int id)
        {
            try
            {
                var entity = _manager.SayisalLotoService.GetOneNumbersArrayByIdAsync(id, false);
                if(entity == null)
                {
                    return NotFound();
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneNumbersArrayAsync([FromBody] SayisalLoto sayisalLoto)
        {
            try
            {
                if (sayisalLoto == null)
                {
                    return BadRequest();
                }
                _manager.SayisalLotoService.CreateOneNumbersArrayAsync(sayisalLoto);
                return StatusCode(201, sayisalLoto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArrayAsync([FromRoute(Name = "id")]int id, [FromBody]SayisalLoto sayisalLoto)
        {
            try
            {
                if (sayisalLoto == null)
                {
                    return BadRequest();
                }

                _manager.SayisalLotoService.UpdateOneNumbersArrayAsync(id, sayisalLoto, true);
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArrayAsync([FromRoute(Name = "id")]int id)
        {
            try
            {
                _manager.SayisalLotoService.DeleteOneNumbersArrayAsync(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
