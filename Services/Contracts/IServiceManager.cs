namespace Services.Contracts
{
    public interface IServiceManager
    {
        ISuperLotoService SuperLotoService { get; }
        ISayisalLotoService SayisalLotoService { get;}
        IOnNumaraService OnNumaraService { get; }

        IAuthenticationService AuthenticationService { get; }
    }
}
