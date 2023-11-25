namespace Entities.DataTransferObjects
{
    public record LotoDto
    {
        public int Id { get; init; }
        public List<int> Numbers { get; init; }
        public DateTime Date { get; init; }

        public LotoDto()
        {
            Numbers = new List<int>();
        }
    }
}
