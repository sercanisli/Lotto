﻿using Entities.DataTransferObjects;
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
    [Route("api/sanstopu")]
    public class SansTopuController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public SansTopuController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpHead]
        [HttpGet("GetAllNumbersArrayForSansTopuAsync", Name = "GetAllNumbersArrayForSansTopuAsync")]
        //[ResponseCache(CacheProfileName = "5mins")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllNumbersArrayForSansTopuAsync([FromQuery] SansTopuParameters sansTopuParameters)
        {
            var linkParameters = new LinkParameters<SansTopuParameters>()
            {
                Parameters = sansTopuParameters,
                HttpContext = HttpContext
            };

            var result = await _manager.SansTopuService.GetAllNumbersArraysAsync(linkParameters,false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }

        [HttpGet("GetRandomNumbersForSansTopuAsync", Name = "GetRandomNumbersForSansTopuAsync")]
        public async Task<IActionResult> GetRandomNumbersForSansTopuAsync()
        {
            var entities = HttpContext.User.Identity?.Name != null ?
                await _manager.SansTopuService.GetRondomNumbersAsync(HttpContext.User.Identity?.Name) :
                await _manager.SansTopuService.GetRondomNumbersAsync(null);

            return Ok(entities);
        }

        [HttpGet("{id:int}")]
        [HttpGet("GetOneNumbersArrayByIdForSansTopuAsync", Name = "GetOneNumbersArrayByIdForSansTopuAsync")]
        //[ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetOneNumbersArrayByIdForSansTopuAsync([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.SansTopuService.GetOneNumbersArrayByIdAsync(id, false);
            return Ok(entity);
        }

        [HttpGet("GetOneNumbersArrayByDateForSansTopuAsync", Name = "GetOneNumbersArrayByDateForSansTopuAsync")]
        //[ResponseCache(CacheProfileName = "5mins")]
        public async Task<IActionResult> GetOneNumbersArrayByDateForSansTopuAsync([FromQuery]DateTime date)
        {
            var entity = await _manager.SansTopuService.GetOneNumbersArrayByDateAsync(date, false);
            return Ok(entity);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneNumbersArrayForSansTopuAsync")]
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
        [HttpPut(Name = "UpdateOneNumbersArrayForSansTopuAsync")]
        public async Task<IActionResult> UpdateOneNumbersArrayForSansTopuAsync([FromRoute(Name = "id")] int id, [FromBody] SansTopuDtoForUpdate sansTopuDtoForUpdate)
        {
            await _manager.SansTopuService.UpdateOneNumbersArrayAsync(id, sansTopuDtoForUpdate, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [HttpDelete(Name = "DeleteOneNumbersArrayForSansTopuAsync")]
        public async Task<IActionResult> DeleteOneNumbersArrayForSansTopuAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.SansTopuService.DeleteOneNumbersArrayAsync(id, false);
            return NoContent();
        }

        [HttpGet("CompareReleasedSansTopuNumbersWithAllSansTopuNumbersAsync", Name = "CompareReleasedSansTopuNumbersWithAllSansTopuNumbersAsync")]
        public async Task<IActionResult> CompareReleasedSansTopuNumbersWithAllSansTopuNumbersAsync([FromBody] SansTopuDtoForCompare sansTopuDtoForCompare)
        {
            var matchRate = await _manager.SansTopuService.CompareSansTopuNumbersAsync(sansTopuDtoForCompare);
            return Ok(matchRate);
        }

        [HttpGet("CompareSansTopuNumbersWithSansTopuLogsNumbersAsync", Name = "CompareSansTopuNumbersWithSansTopuLogsNumbersAsync")]
        public async Task<IActionResult> CompareSansTopuNumbersWithSansTopuLogsNumbersAsync([FromBody] SansTopuDtoForCompareWithLogs sansTopuDtoForCompareWithLogs)
        {
            var matchRate = await _manager.SansTopuService.CompareSansTopuNumbersWithSansTopuLogsNumbersAsync(sansTopuDtoForCompareWithLogs);
            return Ok(matchRate);
        }

        [HttpGet("GetSansTopuLastItemAsync", Name = "GetSansTopuLastItemAsync")]
        public async Task<IActionResult> GetSansTopuLastItemAsync()
        {
            var array = await _manager.SansTopuService.GetLastItemAsync(false);
            return Ok(array);
        }

        [HttpOptions]
        [ResponseCache(CacheProfileName = "5mins")]
        public IActionResult GetSansTopuOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
