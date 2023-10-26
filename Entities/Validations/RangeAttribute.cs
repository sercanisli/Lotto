using System.ComponentModel.DataAnnotations;

namespace Entities.Validations
{
    
    public class RangeAttribute : ValidationAttribute
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public RangeAttribute(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override bool IsValid(object? value)
        {
            if (value is List<int> numbers)
            {
                foreach (var number in numbers)
                {
                    if(number < MinValue ||  number > MaxValue)
                    {
                        ErrorMessage = $"The numbers must be between {MinValue} and {MaxValue}.";
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
