using System.ComponentModel.DataAnnotations;

namespace Entities.Validations
{
    public abstract record SansTopuDtoForManipulation
    {
        [ListLength(5, 5)]
        [RangeAttribute(1, 34)]
        public List<int> Numbers { get; init; }

        public int PlusNumber { get; init; }
        public DateTime Date { get; init; }
    }
}
