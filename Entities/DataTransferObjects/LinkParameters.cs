using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Entities.DataTransferObjects
{
    public record LinkParameters
    {
        public SuperLotoParameters SuperLotoParameters { get; init; }
        public HttpContext HttpContext { get; init; }
    }
}
