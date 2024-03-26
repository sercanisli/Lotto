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
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.GetAllNumbersArrayForOnNumaraAsync), new{pageNumber=1, pageSize=10}),
                        Relation = "onnumara",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.GetAllNumbersArrayForSansTopuAsync), new{pageNumber=1, pageSize=10}),
                        Relation = "sanstopu",
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
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.GetAllNumbersArrayForOnNumaraAsync), new{pageNumber=1, pageSize=10, orderBy="date"}),
                        Relation = "onnumara",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.GetAllNumbersArrayForSansTopuAsync), new{pageNumber=1, pageSize=10, orderBy="date"}),
                        Relation = "sanstopu",
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
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.GetAllNumbersArrayForOnNumaraAsync), new{pageNumber=1, pageSize=10, fields="id,numbers"}),
                        Relation = "onnumara",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.GetAllNumbersArrayForSansTopuAsync), new{pageNumber=1, pageSize=10, fields="id,numbers"}),
                        Relation = "sanstopu",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.GetOneNumbersArrayByIdForSuperLotoAsync), new {}) + "/id",
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.GetOneNumbersArrayByIdForSayisalLotoAsync), new {}) + "/id",
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.GetOneNumbersArrayByIdForOnNumaraAsync), new{}) + "/id",
                        Relation = "onnumara",
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
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.GetOneNumbersArrayByDateForOnNumaraAsync), new{date = "yyyy-mm-dd"}).ToLower(),
                        Relation = "onnumara",
                        Method = "GET"
                    },
                     new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.GetOneNumbersArrayByDateForSansTopuAsync), new{date = "yyyy-mm-dd"}).ToLower(),
                        Relation = "sanstopu",
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
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.CreateOneNumbersArrayForOnNumaraAsync), new{}),
                        Relation = "onnumara",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.CreateOneNumbersArrayForSansTopuAsync), new{}),
                        Relation = "sanstopu",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(WinningNumbersController.CreateOneWinningNumbersAsync), new {}),
                        Relation = "winningnumbers",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(AboutUsController.CreateOneAboutUsAsync), new {}),
                        Relation = "aboutus",
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
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.UpdateOneNumbersArrayForOnNumaraAsync), new{}) + "/id",
                        Relation = "onnumara",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.UpdateOneNumbersArrayForSansTopuAsync), new{}) + "/id",
                        Relation = "sanstopu",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(WinningNumbersController.UpdateOneWinningNumbersAsync), new{}) + "/id",
                        Relation = "winningnumbers",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(AboutUsController.UpdateOneAboutUsAsync), new{}) + "/id",
                        Relation = "aboutus",
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
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.DeleteOneNumbersArrayForOnNumaraAsync), new{}) + "/id",
                        Relation = "onnumara",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.DeleteOneNumbersArrayForSansTopuAsync), new{}) + "/id",
                        Relation = "sanstopu",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(WinningNumbersController.DeleteOneWinningNumbersAsync), new{}) + "/id",
                        Relation = "winningnumbers",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(AboutUsController.DeleteOneAboutUsAsync), new {}) + "/id",
                        Relation = "aboutus",
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
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.GetRandomNumbersForOnNumaraAsync), new{}).ToLower(),
                        Relation = "onnumara",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.GetRandomNumbersForSansTopuAsync), new{}).ToLower(),
                        Relation = "sanstopu",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.CompareRelasedSuperLotoNumbersWithAllSuperLotoNumbersAsync), new{ }).ToLower(),
                        Relation ="superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.CompareReleasedSayisalLotoNumbersWithAllSayisalLotoNumbersAsync), new {}).ToLower(),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.CompareReleasedOnNumaraNumbersWithAllOnNumaraNumbersAsync), new {}).ToLower(),
                        Relation = "onnumara",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.CompareReleasedSansTopuNumbersWithAllSansTopuNumbersAsync), new{}).ToLower(),
                        Relation = "sanstopu",
                        Method = "GET" 
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SuperLotoController.CompareSuperLotoNumbersWithSuperLotoLogsNumbersAsync), new {}).ToLower(),
                        Relation = "superloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SayisalLotoController.CompareSayisalLotoNumbersWithSayisalLotoLogsNumbersAsync), new {}).ToLower(),
                        Relation = "sayisalloto",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(OnNumaraController.CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync), new {}).ToLower(),
                        Relation = "onnumara",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(SansTopuController.CompareSansTopuNumbersWithSansTopuLogsNumbersAsync), new {}).ToLower(),
                        Relation = "sanstopu",
                        Method = "GET"
                    }
                };

                return Ok(list);
            }
            return NoContent();
        }
    }
}
