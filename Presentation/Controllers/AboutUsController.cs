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
        public async Task<IActionResult> GetOneAboutUs([FromRoute(Name = "id")] int id)
        {
            var entity = await _manager.AboutUsService.GetOneAboutUsAsync(id, false);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneAboutUs([FromBody] AboutUsDto aboutUsDto)
        {
            await _manager.AboutUsService.CreateOneAboutUsAsync(aboutUsDto);
            return StatusCode(201, aboutUsDto);
        }

       
    }
}
