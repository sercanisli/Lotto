using Microsoft.AspNetCore.Http;

namespace Entities.LinkModels
{
    public record LinkParameters<T>
    {
        public T Parameters { get; init; }
        public HttpContext HttpContext { get; init; }
    }
}
