namespace Entities.Exceptions
{
    internal class SansTopuDateNotFoundException : NotFoundException
    {
        public SansTopuDateNotFoundException(DateTime date) : base($"Sans Topu with date : {date} could not found.")
        {
        }
    }
}
