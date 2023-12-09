namespace Services.Contracts
{
    public interface IServiceManager
    {
        ISuperLotoService SuperLotoService { get; }
        ISayisalLotoService SayisalLotoService { get;}
        IOnNumaraService OnNumaraService { get; }
        ISansTopuService SansTopuService { get; }

        ISansTopuLogsService SansTopuLogsService { get; }

        IAuthenticationService AuthenticationService { get; }
    }
}
