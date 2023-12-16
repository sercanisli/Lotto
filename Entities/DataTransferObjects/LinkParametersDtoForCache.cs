namespace Entities.DataTransferObjects
{
    public record LinkParametersDtoForCache
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
