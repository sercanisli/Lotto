using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record SansTopuDtoForInsertion : SansTopuDtoForManipulation
    {
        public string Date { get; init; }
        public SansTopuDtoForInsertion()
        {
                
        }
    }
}
