using System.ComponentModel.DataAnnotations;


[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class ListLengthAttribute : ValidationAttribute
{
    public int MinLength { get; }
    public int MaxLength { get; }

    public ListLengthAttribute(int minLength, int maxLength)
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }

    public override bool IsValid(object value)
    {
        if (value is List<int> numbers)
        {
            int length = numbers.Count;
            if (length < MinLength || length > MaxLength)
            {
                ErrorMessage = $"The list must have between {MinLength} and {MaxLength} numbers.";
                return false;
            }
            if (numbers.Distinct().Count() != length)
            {
                ErrorMessage = "The numbers in the list must be different from each other.";
                return false;
            }
            return true;
        }
        return false;
    }
}