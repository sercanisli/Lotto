namespace Entities.Validations
{
    public abstract record SuperLotoDtoForManipulation
    {
        [ListLength(6, 6)]
        public List<int> Numbers { get; init; }
    }
}
