namespace Entities.DataTransferObjects
{
    public record SansTopuDto : LotoDto
    {
        public int PlusNumber { get; init; }
    }
}
