using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface IWinningNumbersService
    {
        Task<WinnigNumbersDto> GetOneWinningNumbersAsync(int id, bool trackchanges);
        Task<WinnigNumbersDto> CreateOneWinningNumbersAsync(WinnigNumbersDto winnigNumbersDto);
        Task UpdateWinningNumbersAsync(int id, WinnigNumbersDto winnigNumbersDto, bool trackchanges);
        Task DeleteWinningNumbersAsync(int id, bool trackChanges);
    }
}
