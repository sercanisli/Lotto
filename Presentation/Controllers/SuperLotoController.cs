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
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/superloto")]
    public class SuperLotoController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public SuperLotoController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpHead]
        [HttpGet(Name = "GetAllNumbersArrayForSuperLotoAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetAllNumbersArrayForSuperLotoAsync([FromQuery]SuperLotoParameters superLotoParameters)
        {
            var linkParameters = new LinkParameters<SuperLotoParameters>()
            {
                Parameters = superLotoParameters,
                HttpContext = HttpContext
            };

            var result = await _manager.SuperLotoService.GetAllNumbersArraysAsync(linkParameters,false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }

        [Authorize(Roles = "Admin, Editor, User")]
        [HttpGet("GetRandomNumbersForSuperLotoAsync", Name = "GetRandomNumbersForSuperLotoAsync")]

        public async Task<IActionResult> GetRandomNumbersForSuperLotoAsync()
        {
            var numbers = HttpContext.User.Identity?.Name != null ?
                await _manager.SuperLotoService.GetRondomNumbersAsync(HttpContext.User.Identity?.Name) :
                await _manager.SuperLotoService.GetRondomNumbersAsync(null);

            return Ok(numbers);
        }

        
        [HttpGet("{id:int}")]
        [ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetOneNumbersArrayByIdForSuperLotoAsync([FromRoute(Name = "id")] int id)
        {
            var array = await _manager.SuperLotoService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(array);
        }

        [HttpGet("GetOneNumbersArrayByDateForSuperLotoAsync", Name = "GetOneNumbersArrayByDateForSuperLotoAsync")]
        [ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetOneNumbersArrayByDateForSuperLotoAsync([FromQuery]DateTime date)
        {
            var array = await _manager.SuperLotoService.GetOneNumbersArrayByDateAsync(date, false);
            return Ok(array);
        }

        [Authorize(Roles = "Admin, Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneNumbersArrayForSuperLotoAsync")]
        public async Task<IActionResult> CreateOneNumbersArrayForSuperLotoAsync([FromBody] SuperLotoDtoForInsertion superLotoDto)
        {
            if (superLotoDto == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var entity = await _manager.SuperLotoService.CreateOneNumbersArrayAsync(superLotoDto);
            return StatusCode(201, entity);
        }

        [Authorize(Roles = "Admin, Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        [HttpPut(Name = "UpdateOneNumbersArrayForSuperLotoAsync")]
        public async Task<IActionResult> UpdateOneNumbersArrayForSuperLotoAsync([FromRoute(Name = "id")] int id, [FromBody] SuperLotoDtoForUpdate superLotoDto)
        {
            if (superLotoDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _manager.SuperLotoService.UpdateOneNumbersArrayAsync(id, superLotoDto, false);
            return NoContent();
        }
        
        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete("{id:int}")]
        [HttpDelete(Name = "DeleteOneNumbersArrayForSuperLotoAsync")]
        public async Task<IActionResult> DeleteOneNumbersArrayForSuperLotoAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.SuperLotoService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }

        [HttpOptions]
        [ResponseCache(CacheProfileName = "5mins")]
        public IActionResult GetSuperLotoOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
