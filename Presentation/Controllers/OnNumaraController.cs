using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/onnumara")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class OnNumaraController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public OnNumaraController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllNumbersArrayForOnNumaraAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllNumbersArrayForOnNumaraAsync([FromQuery]OnNumaraParameters onNumaraParameters)
        {
            var pagedResult = await _manager.OnNumaraService.GetAllNumbersArraysAsync(onNumaraParameters,false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.onNumaraDtos);
        }

        [HttpGet("GetRandomNumbersForOnNumaraAsync")]
        public async Task<IActionResult> GetRandomNumbersForOnNumaraAsync()
        {
            var numbers = await _manager.OnNumaraService.GetRondomNumbersAsync();
            return Ok(numbers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneNumbersArrayByIdForOnNumaraAsync([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.OnNumaraService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(entity);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneNumbersArrayForOnNumaraAsync([FromBody] OnNumaraDtoForInsertion onNumaraDtoForInsertion)
        {
            var entity = await _manager.OnNumaraService.CreateOneNumbersArrayAsync(onNumaraDtoForInsertion);
            return StatusCode(201, entity);
        }
       
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneNumbersArrayForOnNumaraAsync([FromRoute(Name = "id")] int id, [FromBody] OnNumaraDtoForUpdate onNumaraDtoForUpdate)
        {
            await _manager.OnNumaraService.UpdateOneNumbersArrayAsync(id, onNumaraDtoForUpdate, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneNumbersArrayForOnNumaraAsync([FromRoute] int id)
        {
            await _manager.OnNumaraService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }
    }
}
