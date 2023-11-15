namespace Entities.Exceptions
{
    public sealed class OnNumaraNotFoundException : NotFoundException
    {
        public OnNumaraNotFoundException(int id) : base($"The On Numara with id : {id} could not found.")
        {
        }
    }
}
