using Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record SansTopuDtoForUpdate : SansTopuDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
