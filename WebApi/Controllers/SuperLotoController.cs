using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
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
            try
            {
                var numbers = _context.SuperLotos.ToList();
                return Ok(numbers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneNumbersArrayById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var array = _context.SuperLotos.Where(sl => sl.Id == id).SingleOrDefault();
                if (array == null)
                {
                    return NotFound();
                }
                return Ok(array);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneNumbersArray([FromBody]SuperLoto superLoto)
        {
            try
            {
                if(superLoto == null)
                {
                    return BadRequest();
                }
                _context.SuperLotos.Add(superLoto);
                _context.SaveChanges();
                return StatusCode(201, superLoto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneNumbersArray([FromRoute(Name = "id")] int id, [FromBody]SuperLoto superLoto)
        {
            try
            {
                var entity = _context.SuperLotos.Where(sl => sl.Id == id).SingleOrDefault();
                if (entity == null)
                {
                    return NotFound();
                }
                if(id!=superLoto.Id)
                {
                    return BadRequest();
                }
                entity.Numbers = superLoto.Numbers;
                _context.SaveChanges();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneNumbersArray([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _context.SuperLotos.Where(sl => sl.Id == id).SingleOrDefault();
                if (entity == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = $"Array with id:{id} could not found."
                    });
                }
                _context.Remove(entity);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
