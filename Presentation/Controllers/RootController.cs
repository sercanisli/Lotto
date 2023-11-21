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
            string baseUriForSuperLoto = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.CreateOneNumbersArrayForSuperLotoAsync), new {}) +"/1";
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
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetAllNumbersArrayForSuperLotoAsync), new{pageNumber =1, pageSize=10, orderBy = "date"}),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetAllNumbersArrayForSuperLotoAsync), new{pageNumber =1, pageSize=10, fields = "id" + "," + "date"}),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.CreateOneNumbersArrayForSuperLotoAsync), new {}),
                        Relation = "superloto",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.UpdateOneNumbersArrayForSuperLotoAsync), new {}) + "/1",
                        Relation = "superloto",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.DeleteOneNumbersArrayForSuperLotoAsync), new {}) + "/1",
                        Relation = "superloto",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetAllNumbersArrayForSayisalLotoAsync), new{pageNumber =1, pageSize=10}),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetAllNumbersArrayForSayisalLotoAsync), new{pageNumber =1, pageSize=10, orderBy = "date"}),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetAllNumbersArrayForSayisalLotoAsync), new{pageNumber =1, pageSize=10, fields = "id" + "," + "date"}),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.CreateOneNumbersArrayForSayisalLotoAsync), new {}),
                        Relation = "sayisalloto",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.UpdateOneNumbersArrayForSayisalLotoAsync), new {}) + "/1",
                        Relation = "sayisalloto",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.DeleteOneNumbersArrayForSayisalLotoAsync), new {}) + "/1",
                        Relation = "sayisalloto",
                        Method = "DELETE"
                    }

                };

                return Ok(list);
            }
            return NoContent();
        }
    }
}
