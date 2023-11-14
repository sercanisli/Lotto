using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.RequestFeatures;
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
        [HttpGet(Name = "GetAllNumbersArrayForSayisalLotoAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [ResponseCache(CacheProfileName = "5mins")]
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

        [HttpGet("GetRandomNumbersForSayisalLotoAsync")]
        public async Task<IActionResult> GetRandomNumbersForSayisalLotoAsync()
        {
            var numbers = await _manager.SayisalLotoService.GetRondomNumbersAsync();
            return Ok(numbers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneNumbersArrayByIdForSayisalLotoAsync([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.SayisalLotoService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(entity);
        }

        [HttpGet("GetOneNumbersArrayByDateForSayisalLotoAsync")]
        public async Task<IActionResult> GetOneNumbersArrayByDateForSayisalLotoAsync([FromQuery] DateTime date)
        {
            var array = await _manager.SayisalLotoService.GetOneNumbersArrayByDateAsync(date, false);
            return Ok(array);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneNumbersArrayForSayisalLotoAsync([FromBody] SayisalLotoDtoForInsertion sayisalLotoDtoForInsertion)
        {
            var entity = await _manager.SayisalLotoService.CreateOneNumbersArrayAsync(sayisalLotoDtoForInsertion);
            return StatusCode(201, entity);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneNumbersArrayForSayisalLotoAsync([FromRoute(Name = "id")] int id, [FromBody] SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate)
        {
            await _manager.SayisalLotoService.UpdateOneNumbersArrayAsync(id, sayisalLotoDtoForUpdate, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneNumbersArrayForSayisalLotoAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.SayisalLotoService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetSayisalLotoOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
