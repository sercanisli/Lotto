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
        public IActionResult GetAllNumbersArrayForOnNumaraAsync()
        {
            try
            {
                var entities = _manager.OnNumaraService.GetAllNumbersArraysAsync(false);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayByIdForOnNumaraAsync([FromRoute(Name = "id")]int id) 
        {
            try
            {
                var entity = _manager.OnNumaraService.GetOneNumbersArrayByIdAsync(id, false);
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
        public IActionResult CreateOneNumbersArrayForOnNumaraAsync([FromBody]OnNumara onNumara)
        {
            try
            {
                if(onNumara == null)
                {
                    return BadRequest();
                }
                _manager.OnNumaraService.CreateOneNumbersArrayAsync(onNumara);
                return StatusCode(201, onNumara);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArrayForOnNumaraAsync([FromRoute(Name = "id")]int id, [FromBody] OnNumara onNumara)
        {
            try
            {
                if (onNumara == null)
                {
                    return BadRequest();
                }
                _manager.OnNumaraService.UpdateOneNumbersArrayAsync(id, onNumara, true);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArrayForOnNumaraAsync([FromRoute] int id)
        {
            try
            {
                _manager.OnNumaraService.DeleteOneNumbersArrayAsync(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
