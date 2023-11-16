using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record OnNumaraDtoForInsertion : OnNumaraDtoForManipulation
    {
        public string Date { get; init; }
        public OnNumaraDtoForInsertion()
        {
            
        }
    }
}
