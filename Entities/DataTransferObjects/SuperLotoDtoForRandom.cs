namespace Entities.DataTransferObjects
{
    public record SuperLotoDtoForRandom : LotoDtoForRandom
    {
        public string? MatchRate { get; set; }
        public string? Date { get; set; }
    }
}
