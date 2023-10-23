namespace Entities.Exceptions
{
    public sealed class SuperLotoNotFound : NotFound
    {
        public SuperLotoNotFound(string id) : base($"SuperLoto wit id : {id} could not found.")
        {
        }
    }
}
