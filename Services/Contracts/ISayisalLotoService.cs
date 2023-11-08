using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface ISayisalLotoService
    {
        IEnumerable<SayisalLotoDto> GetAllNumbersArraysAsync(bool trackChanges);
        SayisalLotoDto GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        SayisalLotoDto CreateOneNumbersArrayAsync(SayisalLotoDtoForInsertion sayisalLotoDtoForInsertion);
        void DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        void UpdateOneNumbersArrayAsync(int id, SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate, bool trackChanges);
    }
}
