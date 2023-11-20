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
        public async Task<IActionResult> GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            string baseUriForSuperLoto = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.CreateOneNumbersArrayForSuperLotoAsync), new {});
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
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetAllNumbersArrayForSuperLotoAsync), new{pageNumber =1, pageSize=10}),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=baseUriForSuperLoto,
                        Relation = "superloto",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href=baseUriForSuperLoto + "/1",
                        Relation = "superloto",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href=baseUriForSuperLoto + "/1",
                        Relation = "superloto",
                        Method = "DELETE"
                    }

                };

                return Ok(list);
            }
            return NoContent();
        }
    }
}
