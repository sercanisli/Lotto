using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetRoot")]
        public async Task<IActionResult> GetRoot([FromHeader(Name = "Accept")]string mediaType)
        {
            if (mediaType.Contains("application/vnd.lotocum.apiroot"))
            {
                var list = new List<Link>()
                {
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new{}),
                        Relation = "Self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetAllNumbersArrayForSuperLotoAsync), new{}),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.CreateOneNumbersArrayForSuperLotoAsync), new{}),
                        Relation = "superloto",
                        Method = "POST"
                    }
                };

                return Ok(list);
            }
            return NoContent();
        }
    }
}
