namespace Entities.DataTransferObjects
{
    public record SansTopuDtoForRandom : LotoDtoForRandom
    {
        public int PlusNumber { get; init; }
    }
}
