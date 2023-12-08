namespace Entities.DataTransferObjects
{
    public record LotoDtoForRandom
    {
        public List<int> Numbers { get; init; }
        public LotoDtoForRandom()
        {
            Numbers = new List<int>();
        }
    }
}
