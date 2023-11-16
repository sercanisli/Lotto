using Entities.Validations;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record OnNumaraDtoForUpdate : OnNumaraDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
