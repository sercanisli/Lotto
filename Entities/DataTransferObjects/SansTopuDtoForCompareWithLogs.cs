using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record SansTopuDtoForCompareWithLogs
    {
        [ListLength(5, 5)]
        [RangeAttribute(1, 34)]
        public List<int> Numbers { get; init; }
        [RangeAttribute(1, 14)]
        public int PlusNumber { get; set; }
    }
}
