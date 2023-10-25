using System.ComponentModel.DataAnnotations;

namespace Entities.Validations
{
    public abstract record SuperLotoDtoForManipulation
    {
        [ListLength(6, 6)]
        //[Range(1,60)]
        public List<int> Numbers { get; init; }
    }
}
