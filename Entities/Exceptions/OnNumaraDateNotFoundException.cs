namespace Entities.Exceptions
{
    public class OnNumaraDateNotFoundException : NotFoundException
    {
        public OnNumaraDateNotFoundException(DateTime date) : base($"On Numara with date : {date} could not found.")
        {
        }
    }
}
