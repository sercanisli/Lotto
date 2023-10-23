using Microsoft.AspNetCore.Mvc;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperLotoController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public SuperLotoController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllNumbers()
        {
            var numbers = _context.SuperLotos.ToList();
            return Ok(numbers);
        }
    }
}
