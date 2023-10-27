namespace Entities.Exceptions
{
    public sealed class SuperLotoDateNotFoundException : NotFoundException
    {
        public SuperLotoDateNotFoundException(DateTime date) : base($"SuperLoto with date : {date} could not found.")
        {
        }
    }
}
