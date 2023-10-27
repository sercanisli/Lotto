using Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record SuperLotoDtoForInsertion : SuperLotoDtoForManipulation
    {
        public string Date { get; init; }
        public SuperLotoDtoForInsertion()
        {
        }
    }
}
