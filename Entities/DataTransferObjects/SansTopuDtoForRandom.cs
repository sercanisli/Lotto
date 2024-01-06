namespace Entities.DataTransferObjects
{
    public record SansTopuDtoForRandom : LotoDtoForRandom
    {
        public string? MatchRate { get; set; }
        public string? Date { get; set; }
        public int PlusNumber { get; init; }
    }
}
