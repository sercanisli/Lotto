namespace Entities.Exceptions
{
    public sealed class SansTopuNotFoundExceptions : NotFoundException
    {
        public SansTopuNotFoundExceptions(int id) : base($"Sans Topu with id : {id} could not found.")
        {
        }
    }
}
