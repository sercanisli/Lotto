namespace Entities.DataTransferObjects
{
    public record SayisalLotoDtoForUpdate
    {
        public int Id { get; init; }
        public List<int> Numbers { get; init; }
        public DateTime Date { get; init; }

        public SayisalLotoDtoForUpdate()
        {
            Numbers = new List<int>();
        }
    }
}
