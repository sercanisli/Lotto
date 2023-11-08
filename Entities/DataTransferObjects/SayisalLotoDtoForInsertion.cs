using Entities.Validations;

namespace Entities.DataTransferObjects
{
    public record SayisalLotoDtoForInsertion : SayisalLotoDtoForManipulation
    {
        public string Date { get; init; }
        public SayisalLotoDtoForInsertion()
        {
            
        }
    }
}
