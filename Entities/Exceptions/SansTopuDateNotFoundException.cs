namespace Entities.Exceptions
{
    public class SansTopuDateNotFoundException : NotFoundException
    {
        public SansTopuDateNotFoundException(DateTime date) : base($"Sans Topu with date : {date} could not found.")
        {
        }
    }
}
