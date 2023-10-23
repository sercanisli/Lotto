namespace Entities.DataTransferObjects
{
    public record SuperLotoDtoForUpdate
    {
        public int Id { get; init; }
        public List<int> Numbers { get; init; }

        public SuperLotoDtoForUpdate()
        {
            Numbers = new List<int>();
        }
    }
}
