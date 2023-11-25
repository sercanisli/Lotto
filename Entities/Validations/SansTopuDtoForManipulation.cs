namespace Entities.Validations
{
    public class SansTopuDtoForManipulation
    {
        [ListLength(5, 5)]
        [RangeAttribute(1, 34)]
        public List<int> Numbers { get; init; }

        [RangeAttribute(1, 14)]
        public int PlusNumber { get; set; }
        public DateTime Date { get; init; }
    }
}
