using Entities.DataTransferObjects;
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
            var entities = _manager.SansTopuService.GetAllNumbersArrays(false);
            return Ok(entities);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayByIdForSansTopu([FromRoute(Name = "id")] int id)
        {
            var entity = _manager.SansTopuService.GetOneNumbersArrayById(id, false);
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult CreateOneNumbersArrayForSansTopu([FromBody] SansTopuDtoForInsertion sansTopuDtoForInsertion)
        {
            var entity = _manager.SansTopuService.CreateOneNumbersArray(sansTopuDtoForInsertion);
            return StatusCode(201, entity);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArrayForSansTopu([FromRoute(Name = "id")] int id, [FromBody] SansTopuDtoForUpdate sansTopuDtoForUpdate)
        {
            _manager.SansTopuService.UpdateOneNumbersArray(id, sansTopuDtoForUpdate, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArrayForSansTopu([FromRoute(Name = "id")] int id)
        {
            _manager.SansTopuService.DeleteOneNumbersArray(id, false);
            return NoContent();
        }
    }
}
