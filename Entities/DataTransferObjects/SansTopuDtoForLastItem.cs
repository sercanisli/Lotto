namespace Entities.DataTransferObjects
{
    public record SansTopuDtoForLastItem : LotoDto
    {
        public int PlusNumber { get; init; }
    }
}
