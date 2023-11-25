using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
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
        public async Task<IActionResult> GetAllNumbersArrayForSansTopuAsync()
        {
            var entities = await _manager.SansTopuService.GetAllNumbersArraysAsync(false);
            return Ok(entities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneNumbersArrayByIdForSansTopuAsync([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.SansTopuService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(entity);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneNumbersArrayForSansTopuAsync([FromBody] SansTopuDtoForInsertion sansTopuDtoForInsertion)
        {
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var entity = await _manager.SansTopuService.CreateOneNumbersArrayAsync(sansTopuDtoForInsertion);
            return StatusCode(201, entity);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneNumbersArrayForSansTopuAsync([FromRoute(Name = "id")] int id, [FromBody] SansTopuDtoForUpdate sansTopuDtoForUpdate)
        {
            await _manager.SansTopuService.UpdateOneNumbersArrayAsync(id, sansTopuDtoForUpdate, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneNumbersArrayForSansTopuAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.SansTopuService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }
    }
}
