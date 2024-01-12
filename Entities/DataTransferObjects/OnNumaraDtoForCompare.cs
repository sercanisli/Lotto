using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record OnNumaraDtoForCompare
    {
        [ListLength(22, 22)]
        [RangeAttribute(1, 80)]
        public List<int> Numbers { get; init; }
    }
}
