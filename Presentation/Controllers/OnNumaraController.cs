using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
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

        [HttpHead]
        [HttpGet(Name = "GetAllNumbersArrayForOnNumaraAsync")]
        //[ResponseCache(CacheProfileName = "5mins")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllNumbersArrayForOnNumaraAsync([FromQuery]OnNumaraParameters onNumaraParameters)
        {
            var linkParameters = new LinkParameters<OnNumaraParameters>()
            {
                Parameters = onNumaraParameters,
                HttpContext = HttpContext
            };

            var result = await _manager.OnNumaraService.GetAllNumbersArraysAsync(linkParameters,false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }

        [HttpGet("GetRandomNumbersForOnNumaraAsync", Name = "GetRandomNumbersForOnNumaraAsync")]
        //[Authorize(Roles = "Admin, Editor, User")]
        public async Task<IActionResult> GetRandomNumbersForOnNumaraAsync()
        {
            var numbers = HttpContext.User.Identity?.Name != null ?
                await _manager.OnNumaraService.GetRondomNumbersAsync(HttpContext.User.Identity?.Name) :
                await _manager.OnNumaraService.GetRondomNumbersAsync(null);

            return Ok(numbers);
        }

        [HttpGet("{id:int}")]
        //[ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetOneNumbersArrayByIdForOnNumaraAsync([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.OnNumaraService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(entity);
        }

        [HttpGet("GetOneNumbersArrayByDateForOnNumaraAsync", Name = "GetOneNumbersArrayByDateForOnNumaraAsync")]
        //[ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetOneNumbersArrayByDateForOnNumaraAsync([FromQuery]DateTime date)
        {
            var entity = await _manager.OnNumaraService.GetOneNumbersArrayByDateAsync(date, false);
            return Ok(entity);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneNumbersArrayForOnNumaraAsync")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> CreateOneNumbersArrayForOnNumaraAsync([FromBody] OnNumaraDtoForInsertion onNumaraDtoForInsertion)
        {
            var entity = await _manager.OnNumaraService.CreateOneNumbersArrayAsync(onNumaraDtoForInsertion);
            return StatusCode(201, entity);
        }
       
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        [HttpPut(Name = "UpdateOneNumbersArrayForOnNumaraAsync")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> UpdateOneNumbersArrayForOnNumaraAsync([FromRoute(Name = "id")] int id, [FromBody] OnNumaraDtoForUpdate onNumaraDtoForUpdate)
        {
            await _manager.OnNumaraService.UpdateOneNumbersArrayAsync(id, onNumaraDtoForUpdate, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [HttpDelete(Name = "DeleteOneNumbersArrayForOnNumaraAsync")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> DeleteOneNumbersArrayForOnNumaraAsync([FromRoute] int id)
        {
            await _manager.OnNumaraService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }

        [HttpGet("CompareReleasedOnNumaraNumbersWithAllOnNumaraNumbersAsync", Name = "CompareReleasedOnNumaraNumbersWithAllOnNumaraNumbersAsync")]
        public async Task<IActionResult> CompareReleasedOnNumaraNumbersWithAllOnNumaraNumbersAsync([FromBody] OnNumaraDtoForCompare onNumaraDtoForCompare)
        {
            var matchRate = await _manager.OnNumaraService.CompareOnNumaraNumbersAsync(onNumaraDtoForCompare);
            return Ok(matchRate);
        }

        [HttpGet("CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync", Name = "CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync")]
        public async Task<IActionResult> CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync([FromBody] OnNumaraDtoForCompareWithLogs onNumaraDtoForCompareWithLogs)
        {
            var matchRate = await _manager.OnNumaraService.CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync(onNumaraDtoForCompareWithLogs);
            return Ok(matchRate);
        }

        [HttpGet("GetOnNumaraLastItemAsync", Name = "GetOnNumaraLastItemAsync")]
        public async Task<IActionResult> GetOnNumaraLastItemAsync()
        {
            var array = await _manager.OnNumaraService.GetLastItemAsync(false);
            return Ok(array);
        }

        [HttpOptions]
        [ResponseCache(CacheProfileName = "5mins")]
        public IActionResult GetOnNumaraOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
