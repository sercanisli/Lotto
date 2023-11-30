using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface ISansTopuLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<SansTopuDto> sansTopuDtos, string fields, HttpContext context);
    }
}
