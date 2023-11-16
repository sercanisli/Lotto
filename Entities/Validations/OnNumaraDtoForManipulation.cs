namespace Entities.Validations
{
    public class OnNumaraDtoForManipulation
    {
        [ListLength(10, 10)]
        [RangeAttribute(1, 80)]
        public List<int> Numbers { get; init; }
        public DateTime Date { get; init; }
    }
}
