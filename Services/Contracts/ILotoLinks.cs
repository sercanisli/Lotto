using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface ILotoLinks<T>
    {
        LinkResponse TryGenerateLinks(IEnumerable<T> dtos, string fields, HttpContext context);
    }
}
