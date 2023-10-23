using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            try
            {
                var numbers = _manager.SuperLotoService.GetAllNumbersArrays(false);
                return Ok(numbers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var array = _manager.SuperLotoService.GetOneNumbersArrayById(id, false);
                if (array == null)
                {
                    return NotFound();
                }
                return Ok(array);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneNumbersArray([FromBody]SuperLoto superLoto)
        {
            try
            {
                if(superLoto == null)
                {
                    return BadRequest();
                }
                _manager.SuperLotoService.CreateOneNumbersArray(superLoto);
                return StatusCode(201, superLoto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArray([FromRoute(Name = "id")] int id, [FromBody]SuperLoto superLoto)
        {
            try
            {
                if (superLoto == null)
                {
                    return BadRequest();
                }
                _manager.SuperLotoService.UpdateOneNumbersArray(id, superLoto,true);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArray([FromRoute(Name = "id")] int id)
        {
            try
            {
                
                _manager.SuperLotoService.DeleteOneNumbersArray(id,false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
