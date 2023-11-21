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
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetAllNumbersArrayForSuperLotoAsync), new{pageNumber=1, pageSize=10}),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetAllNumbersArrayForSayisalLotoAsync), new{pageNumber=1, pageSize=10}),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetAllNumbersArrayForSuperLotoAsync), new{pageNumber=1, pageSize=10, orderBy="date"}),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetAllNumbersArrayForSayisalLotoAsync), new{pageNumber=1, pageSize=10, orderBy="date"}),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetAllNumbersArrayForSuperLotoAsync), new{pageNumber=1, pageSize=10, fields = "id,numbers"}),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetAllNumbersArrayForSayisalLotoAsync), new{pageNumber=1, pageSize=10, fields="id,numbers"}),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetOneNumbersArrayByDateForSuperLotoAsync), new{date = "yyyy-mm-dd"}).ToLower(),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetOneNumbersArrayByDateForSayisalLotoAsync), new{date = "yyyy-mm-dd"}).ToLower(),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.CreateOneNumbersArrayForSuperLotoAsync), new{}),
                        Relation = "superloto",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.CreateOneNumbersArrayForSayisalLotoAsync), new{}),
                        Relation = "sayisalloto",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.UpdateOneNumbersArrayForSuperLotoAsync), new{}) + "/id",
                        Relation = "superloto",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.UpdateOneNumbersArrayForSayisalLotoAsync), new{}) + "/id",
                        Relation = "sayisalloto",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.DeleteOneNumbersArrayForSuperLotoAsync), new{}) + "/id",
                        Relation = "superloto",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.DeleteOneNumbersArrayForSayisalLotoAsync), new{}) + "/id",
                        Relation = "sayisalloto",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetRandomNumbersForSuperLotoAsync), new{}).ToLower(),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetRandomNumbersForSayisalLotoAsync), new{}).ToLower(),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                };

                return Ok(list);
            }
            return NoContent();
        }
    }
}
