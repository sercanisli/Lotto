using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/winningnumbers")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public class WinningNumbersController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public WinningNumbersController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneWinningNumbersAsync([FromRoute(Name = "id")]int id)
        {
            var entity = await _manager.WinningNumbersService.GetOneWinningNumbersAsync(id, false);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneWinningNumbersAsync([FromBody] WinnigNumbersDto winnigNumbersDto)
        {
            var entity = await _manager.WinningNumbersService.CreateOneWinningNumbersAsync(winnigNumbersDto);
            return StatusCode(201, entity);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneWinningNumbersAsync([FromRoute(Name = "id")] int id, [FromBody] WinnigNumbersDto winnigNumbersDto)
        {
            await _manager.WinningNumbersService.UpdateWinningNumbersAsync(id, winnigNumbersDto, false);
            return NoContent();
        }
    }
}
