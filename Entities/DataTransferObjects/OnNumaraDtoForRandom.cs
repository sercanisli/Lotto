namespace Entities.DataTransferObjects
{
    public record OnNumaraDtoForRandom : LotoDtoForRandom
    {
        public string? MatchRate  { get; set; }
        public string? Date { get; set; }

    }
}
