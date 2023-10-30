using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface ISuperLotoLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<SuperLotoDto> superLotoDto, string fields, HttpContext context);
    }
}
