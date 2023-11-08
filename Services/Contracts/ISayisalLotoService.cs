using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface ISayisalLotoService
    {
        Task<IEnumerable<SayisalLotoDto>> GetAllNumbersArraysAsync(bool trackChanges);
        Task<SayisalLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SayisalLotoDto> CreateOneNumbersArrayAsync(SayisalLotoDtoForInsertion sayisalLotoDtoForInsertion);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task UpdateOneNumbersArrayAsync(int id, SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate, bool trackChanges);
    }
}
