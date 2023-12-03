namespace Entities.DataTransferObjects
{
    public record SansTopuDtoForRandom
    {

        public int PlusNumber { get; init; }
        public List<int> Numbers { get; init; }

        public SansTopuDtoForRandom()
        {
            Numbers = new List<int>();
        }

    }
}
