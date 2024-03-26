using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/aboutus")]
    public class AboutUsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public AboutUsController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("{id:int}")]
        [HttpGet(Name = "GetOneAboutUsAsync")]
        public async Task<IActionResult> GetOneAboutUsAsync([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.AboutUsService.GetOneAboutUsAsync(id, false);
            return Ok(entity);
        }

        [HttpPost(Name = "CreateOneAboutUsAsync")]
        public async Task<IActionResult> CreateOneAboutUsAsync([FromBody] AboutUsDto aboutUsDto)
        {
            await _manager.AboutUsService.CreateOneAboutUsAsync(aboutUsDto);
            return StatusCode(201, aboutUsDto);
        }

        [HttpPut("{id:int}")]
        [HttpPut(Name = "UpdateOneAboutUsAsync")]
        public async Task<IActionResult> UpdateOneAboutUsAsync([FromRoute(Name = "id")] int id, [FromBody] AboutUsDto aboutUsDto)
        {
            await _manager.AboutUsService.UpdateAboutUsAsync(id, aboutUsDto, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [HttpDelete(Name = "DeleteOneAboutUsAsync")]
        public async Task<IActionResult> DeleteOneAboutUsAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.AboutUsService.DeleteAboutUsAsync(id, false);
            return NoContent();
        }
    }
}
