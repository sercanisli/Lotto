using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;

namespace Services.Concrete
{
    public class OnNumaraLinks : LotoLinks<OnNumaraDto>, IOnNumaraLinks
    {
        public OnNumaraLinks(LinkGenerator linkGenerator, IDataShaper<OnNumaraDto> dataShaper) : base(linkGenerator, dataShaper)
        {
        }
    }
}
