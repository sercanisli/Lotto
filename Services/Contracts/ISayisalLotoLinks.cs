using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface ISayisalLotoLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<SayisalLotoDto> sayisalLotoDto, string fields, HttpContext context);
    }
}
