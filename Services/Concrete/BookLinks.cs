using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Contracts;

namespace Services.Concrete
{
    public class BookLinks : IBookLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<SuperLotoDto> _dataShaper;

        public BookLinks(LinkGenerator linkGenerator, IDataShaper<SuperLotoDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<SuperLotoDto> superLotoDto, string fields, HttpContext context)
        {
            var shapedEntities = ShapeData(superLotoDto, fields);
            if(ShouldGenerateLinks(context))
            {
                return ReturnLinkedEntities(superLotoDto, fields, context, shapedEntities);
            }
            return ReturnShapedEntities(shapedEntities);
        }

        private LinkResponse ReturnLinkedEntities(IEnumerable<SuperLotoDto> superLotoDto, string fields, HttpContext context, List<Entity> shapedEntities)
        {
            var superLotoDtoList = superLotoDto.ToList();
            for (int i = 0; i < superLotoDtoList.Count(); i++)
            {
                var superLotoLinks = CreateForSuperLoto(context, superLotoDtoList[i], fields);
                shapedEntities[i].Add("Links", superLotoLinks);
            }
            var superLotoCollection = new LinkCollectionWrapper<Entity>(shapedEntities);
            return new LinkResponse
            {
                HasLinks = true,
                LinkedEntities = superLotoCollection
            };
        }

        private List<Link> CreateForSuperLoto(HttpContext context, SuperLotoDto superLotoDto, string fields)
        {
            var links = new List<Link>()
            {
                new Link("a1","b1","c1"),
                new Link("a2","b2","c2")
            };
            return links;
        }

        private LinkResponse ReturnShapedEntities(List<Entity> shapedEntities)
        {
            return new LinkResponse() { ShapedEntity = shapedEntities };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<SuperLotoDto> superLotoDto, string fields)
        {
            return _dataShaper.ShapeData(superLotoDto, fields).Select(sl => sl.Entity).ToList();
        }
    }
}
