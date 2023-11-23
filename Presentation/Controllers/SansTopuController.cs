using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/sanstopu")]
    public class SansTopuController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public SansTopuController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllNumbersArrayForSansTopu()
        {
            try
            {
                var entities = _manager.SansTopuService.GetAllNumbersArrays(false);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayByIdForSansTopu([FromRoute(Name = "id")]int id)
        {
            try
            {
                var entity = _manager.SansTopuService.GetOneNumbersArrayById(id, false);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneNumbersArrayForSansTopu([FromBody] SansTopu sansTopu)
        {
            try
            {
                _manager.SansTopuService.CreateOneNumbersArray(sansTopu);
                return StatusCode(201, sansTopu);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArrayForSansTopu([FromRoute(Name = "id")]int id, [FromBody]SansTopu sansTopu)
        {
            try
            {
                _manager.SansTopuService.UpdateOneNumbersArray(id,sansTopu,false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArrayForSansTopu([FromRoute(Name = "id")]int id)
        {
            try
            {
                _manager.SansTopuService.DeleteOneNumbersArray(id, false);
                return NoContent();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
