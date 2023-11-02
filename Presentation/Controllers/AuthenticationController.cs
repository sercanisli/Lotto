using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Linq.Dynamic.Core.Tokenizer;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public AuthenticationController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserDtoForRegistration userDtoForRegistration)
        {
            var result = await _manager.AuthenticationService.RegisterUser(userDtoForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody]UserDtoForAuthentication userDtoForAuthentication)
        {
            if(!await _manager.AuthenticationService.ValidateUser(userDtoForAuthentication))
            {
                return Unauthorized();
            }
            return Ok(new
            {
                Token = await _manager.AuthenticationService.CreateToken()
            });
        }
    }
}
