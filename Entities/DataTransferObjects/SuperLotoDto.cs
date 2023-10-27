namespace Entities.DataTransferObjects
{
    [Serializable]
    public record SuperLotoDto
    {
        public int Id { get; init; }
        public List<int> Numbers { get; init; }
        public DateTime Date { get; init; }

        public SuperLotoDto()
        {
            Numbers = new List<int>();
        }
    }
}
