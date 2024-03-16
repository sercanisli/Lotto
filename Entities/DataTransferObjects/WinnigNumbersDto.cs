namespace Entities.DataTransferObjects
{
    public record WinnigNumbersDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }
}
