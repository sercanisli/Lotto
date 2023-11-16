using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record OnNumaraDtoForUpdate
    {
        public int Id { get; init; }

        [ListLength(10, 10)]
        [RangeAttribute(1, 80)]
        public List<int> Numbers { get; init; }
        public DateTime? Date { get; init; }
    }
}
