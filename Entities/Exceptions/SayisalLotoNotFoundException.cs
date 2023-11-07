namespace Entities.Exceptions
{
    public sealed class SayisalLotoNotFoundException : NotFoundException
    {
        public SayisalLotoNotFoundException(int id) : base($"The Sayisal Loto with id : {id} could not found.")
        {
        }
    }
}
