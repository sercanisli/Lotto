using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Cantracts;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperLotoController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public SuperLotoController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllNumbersArray()
        {
            try
            {
                var numbers = _manager.SuperLoto.GetAllNumbersArray(false);
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
                var array = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
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
                _manager.SuperLoto.CreateOneNumbersArray(superLoto);
                _manager.Save();
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
                var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, true);
                if (entity == null)
                {
                    return NotFound();
                }
                if(id!=superLoto.Id)
                {
                    return BadRequest();
                }
                entity.Numbers = superLoto.Numbers;
                _manager.Save();
                return Ok(entity);
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
                var entity = _manager.SuperLoto.GetOneNumbersArrayById(id, false);
                if (entity == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = $"Array with id:{id} could not found."
                    });
                }
                _manager.SuperLoto.DeleteOneNumbersArray(entity);
                _manager.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
