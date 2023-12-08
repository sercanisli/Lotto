namespace Entities.DataTransferObjects
{
    public record OnNumaraDtoForRandom
    {
        public List<int> Numbers { get; init; }

        public OnNumaraDtoForRandom()
        {
            Numbers = new List<int>();
        }
    }
}
