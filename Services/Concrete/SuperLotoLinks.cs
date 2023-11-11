using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;

namespace Services.Concrete
{
    public class SuperLotoLinks : LotoLinks<SuperLotoDto>, ISuperLotoLinks
    {
        public SuperLotoLinks(LinkGenerator linkGenerator, IDataShaper<SuperLotoDto> dataShaper) : base(linkGenerator, dataShaper)
        {
        }
    }
}
