namespace Services.Contracts
{
    public interface IServiceManager
    {
        ISuperLotoService SuperLotoService { get; }
        ISayisalLotoService SayisalLotoService { get;}
        IOnNumaraService OnNumaraService { get; }
        ISansTopuService SansTopuService { get; }
        IWinningNumbersService WinningNumbersService { get; }


        IAuthenticationService AuthenticationService { get; }
    }
}
