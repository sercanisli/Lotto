namespace Entities.DataTransferObjects
{
    public record SayisalLotoDtoForRandom : LotoDtoForRandom
    {
        public string? MatchRate { get; set; }
    }
}
