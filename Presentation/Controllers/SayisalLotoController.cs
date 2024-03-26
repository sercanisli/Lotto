using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/sayisalloto")]
    public class SayisalLotoController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public SayisalLotoController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpHead]
        [HttpGet(Name ="GetAllNumbersArrayForSayisalLotoAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //[ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetAllNumbersArrayForSayisalLotoAsync([FromQuery] SayisalLotoParameters sayisalLotoParameters)
        {
            var linkParameters = new LinkParameters<SayisalLotoParameters>()
            {
                Parameters = sayisalLotoParameters,
                HttpContext = HttpContext
            };

            var result = await _manager.SayisalLotoService.GetAllNumbersArraysAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }

        //[Authorize(Roles = "Admin, Editor, User")]
        [HttpGet("GetRandomNumbersForSayisalLotoAsync", Name = "GetRandomNumbersForSayisalLotoAsync")]
        public async Task<IActionResult> GetRandomNumbersForSayisalLotoAsync()
        {
            var numbers = HttpContext.User.Identity?.Name != null ?
                await _manager.SayisalLotoService.GetRondomNumbersAsync(HttpContext.User.Identity?.Name) :
                await _manager.SayisalLotoService.GetRondomNumbersAsync(null);

            return Ok(numbers);
        }

        [HttpGet("{id:int}")]
        [HttpGet(Name = "GetOneNumbersArrayByIdForSayisalLotoAsync")]
        //[ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetOneNumbersArrayByIdForSayisalLotoAsync([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.SayisalLotoService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(entity);
        }

        [HttpGet("GetOneNumbersArrayByDateForSayisalLotoAsync", Name ="GetOneNumbersArrayByDateForSayisalLotoAsync")]
        //[ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetOneNumbersArrayByDateForSayisalLotoAsync([FromQuery] DateTime date)
        {
            var array = await _manager.SayisalLotoService.GetOneNumbersArrayByDateAsync(date, false);
            return Ok(array);
        }

        [Authorize(Roles = "Admin, Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneNumbersArrayForSayisalLotoAsync")]
        public async Task<IActionResult> CreateOneNumbersArrayForSayisalLotoAsync([FromBody] SayisalLotoDtoForInsertion sayisalLotoDtoForInsertion)
        {
            var entity = await _manager.SayisalLotoService.CreateOneNumbersArrayAsync(sayisalLotoDtoForInsertion);
            return StatusCode(201, entity);
        }

        [Authorize(Roles = "Admin, Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        [HttpPut(Name = "UpdateOneNumbersArrayForSayisalLotoAsync")]
        public async Task<IActionResult> UpdateOneNumbersArrayForSayisalLotoAsync([FromRoute(Name = "id")] int id, [FromBody] SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate)
        {
            await _manager.SayisalLotoService.UpdateOneNumbersArrayAsync(id, sayisalLotoDtoForUpdate, false);
            return NoContent();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete("{id:int}")]
        [HttpDelete(Name = "DeleteOneNumbersArrayForSayisalLotoAsync")]
        public async Task<IActionResult> DeleteOneNumbersArrayForSayisalLotoAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.SayisalLotoService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }

        [HttpGet("CompareReleasedSayisalLotoNumbersWithAllSayisalLotoNumbersAsync", Name = "CompareReleasedSayisalLotoNumbersWithAllSayisalLotoNumbersAsync")]
        public async Task<IActionResult> CompareReleasedSayisalLotoNumbersWithAllSayisalLotoNumbersAsync([FromBody] SayisalLotoDtoForCompare sayisalLotoDtoForCompare)
        {
            var matchRate = await _manager.SayisalLotoService.CompareSayisalLotoNumbersAsync(sayisalLotoDtoForCompare);
            return Ok(matchRate);
        }

        [HttpGet("CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync", Name = "CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync")]
        public async Task<IActionResult> CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync([FromBody] SayisalLotoDtoForCompareWithLogs sayisalLotoDtoForCompareWithLogs)
        {
            var matchRate = await _manager.SayisalLotoService.CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync(sayisalLotoDtoForCompareWithLogs);
            return Ok(matchRate);
        }

        [HttpGet("GetSayisalLotoLastItemAsync", Name = "GetSayisalLotoLastItemAsync")]
        public async Task<IActionResult> GetSayisalLotoLastItemAsync()
        {
            var array = await _manager.SayisalLotoService.GetLastItemAsync(false);
            return Ok(array);
        }

        [HttpOptions]
        [ResponseCache(CacheProfileName = "5mins")]
        public IActionResult GetSayisalLotoOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
