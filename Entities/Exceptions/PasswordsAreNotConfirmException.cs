namespace Entities.Exceptions
{
    public class PasswordsAreNotConfirmException : BadRequestException
    {
        public PasswordsAreNotConfirmException() : base($"Password and Confirm Password are not match")
        {
        }
    }
}
