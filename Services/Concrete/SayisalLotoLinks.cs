using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;

namespace Services.Concrete
{
    public class SayisalLotoLinks : LotoLinks<SayisalLotoDto>, ISayisalLotoLinks
    {
        public SayisalLotoLinks(LinkGenerator linkGenerator, IDataShaper<SayisalLotoDto> dataShaper) : base(linkGenerator, dataShaper)
        {
        }
    }
}
