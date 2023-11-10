using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Contracts;

namespace Services.Concrete
{
    public class SayisalLotoLinks : ISayisalLotoLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<SayisalLotoDto> _dataShaper;

        public SayisalLotoLinks(LinkGenerator linkGenerator, IDataShaper<SayisalLotoDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<SayisalLotoDto> sayisalLotoDto, string fields, HttpContext context)
        {
            var shapedSayisalLotos = ShapeData(sayisalLotoDto, fields);
            if (ShouldGenerateLinks(context))
            {
                return ReturnLinkedEntities(sayisalLotoDto, fields, context, shapedSayisalLotos);
            }
            return ReturnShapedEntities(shapedSayisalLotos);
        }

        private LinkResponse ReturnLinkedEntities(IEnumerable<SayisalLotoDto> sayisalLotoDto, string fields, HttpContext context, List<Entity> shapedSayisalLotos)
        {
            var sayisalLotoDtoList = sayisalLotoDto.ToList();
            for(int index = 0; index<sayisalLotoDtoList.Count(); index++)
            {
                var sayisalLotoLinks = CreateForSayisalLoto(context, sayisalLotoDtoList[index], fields);
                shapedSayisalLotos[index].Add("Links", sayisalLotoLinks);
            }
            var sayisalLotoCollection = new LinkCollectionWrapper<Entity>(shapedSayisalLotos);

            return new LinkResponse 
            { 
                HasLinks = true, 
                LinkedEntities = sayisalLotoCollection 
            };
        }

        private List<Link> CreateForSayisalLoto(HttpContext context, SayisalLotoDto sayisalLotoDto, string fields)
        {
            var links = new List<Link>()
            {
                new Link("a1","b1","c1"),
                new Link("a2","b2","c2")
            };
            return links;
        }

        private LinkResponse ReturnShapedEntities(List<Entity> shapedSayisalLotos)
        {
            return new LinkResponse() { ShapedEntities = shapedSayisalLotos };
        }

        private bool ShouldGenerateLinks(HttpContext context)
        {
            var mediaType = (MediaTypeHeaderValue)context.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateos", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<SayisalLotoDto> sayisalLotoDto, string fields)
        {
            return _dataShaper.ShapeData(sayisalLotoDto, fields).Select(sl => sl.Entity).ToList();
        }
    }
}
