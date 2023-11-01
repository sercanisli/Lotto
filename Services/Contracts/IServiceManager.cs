namespace Services.Contracts
{
    public interface IServiceManager
    {
        ISuperLotoService SuperLotoService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
