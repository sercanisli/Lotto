﻿using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Contracts;

namespace Services.Concrete
{
    public class LotoLinks<T> : ILotoLinks<T>
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<T> _dataShaper;

        public LotoLinks(LinkGenerator linkGenerator, IDataShaper<T> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<T> dtos, string fields, HttpContext context)
        {
            var shapedData = ShapeData(dtos, fields);
            if (ShouldGenerateLinks(context))
            {
                return ReturnLinkedEntities(dtos, fields, context, shapedData);
            }
            return ReturnShapedEntities(shapedData);
        }

        private LinkResponse ReturnShapedEntities(List<Entity> shapedData)
        {
            return new LinkResponse { ShapedEntities = shapedData };
        }

        private LinkResponse ReturnLinkedEntities(IEnumerable<T> dtos, string fields, HttpContext context, List<Entity> shapedData)
        {
            var dtoList = dtos.ToList();
            for(int index =0; index < dtoList.Count(); index++)
            {
                var links = CreateLinks(context, dtoList[index], fields);
                shapedData[index].Add("Links", links);
            }
            var collection = new LinkCollectionWrapper<Entity>(shapedData);

            return new LinkResponse
            {
                HasLinks = true,
                LinkedEntities = collection
            };
        }

        private List<Link> CreateLinks(HttpContext context, T dto, string fields)
        {
            var links = new List<Link>()
            {
                new Link("a1","b1","c1"),
                new Link("a2","b2","c2")
            };
            return links;
        }

        private bool ShouldGenerateLinks(HttpContext context)
        {
            var mediaType = (MediaTypeHeaderValue)context.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateos", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<T> dtos, string fields)
        {
            return _dataShaper.ShapeData(dtos,fields).Select(d=>d.Entity).ToList();
        }
    }
}
