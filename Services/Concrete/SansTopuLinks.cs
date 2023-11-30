using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;

namespace Services.Concrete
{
    public class SansTopuLinks : LotoLinks<SansTopuDto>, ISansTopuLinks
    {
        public SansTopuLinks(LinkGenerator linkGenerator, IDataShaper<SansTopuDto> dataShaper) : base(linkGenerator, dataShaper)
        {
        }
    }
}
