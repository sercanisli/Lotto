namespace Entities.Validations
{
    public abstract record OnNumaraDtoForManipulation
    {
        [ListLength(22, 22)]
        [RangeAttribute(1, 80)]
        public List<int> Numbers { get; init; }
        public DateTime Date { get; init; }
    }
}
