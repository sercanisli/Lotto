namespace Entities.Exceptions
{
    public sealed class SuperLotoNotFoundException : NotFoundException
    {
        public SuperLotoNotFoundException(int id) : base($"SuperLoto with id : {id} could not found.")
        {
        }
    }
}
