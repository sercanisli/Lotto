using Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record SayisalLotoDtoForUpdate : SayisalLotoDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
