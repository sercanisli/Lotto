using Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record SuperLotoDtoForUpdate : SuperLotoDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
