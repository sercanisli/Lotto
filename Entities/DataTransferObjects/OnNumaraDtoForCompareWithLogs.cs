using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record OnNumaraDtoForCompareWithLogs
    {
        [ListLength(22, 22)]
        [Range(1, 80)]
        public List<int> Numbers { get; init; }
    }
}
