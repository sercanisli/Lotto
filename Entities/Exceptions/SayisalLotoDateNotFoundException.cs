namespace Entities.Exceptions
{
    public class SayisalLotoDateNotFoundException : NotFoundException
    {
        public SayisalLotoDateNotFoundException(DateTime date) : base($"Sayisal Loto with date : {date} could not found.")
        {
        }
    }
}
