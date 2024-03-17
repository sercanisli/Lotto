namespace Entities.DataTransferObjects
{
    public record AboutUsDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }
}
