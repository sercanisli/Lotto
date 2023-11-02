namespace Entities.Exceptions
{
    public class RefreshTokenBadRequestException : BadRequestException
    {
        public RefreshTokenBadRequestException() : base($"Invalid client request. TokenDto has some invalid values.")
        {
        }
    }
}
