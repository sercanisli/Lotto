using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface ISayisalLotoService
    {
        Task<(IEnumerable<SayisalLotoDto> sayisalLotos, MetaData metaData)> GetAllNumbersArraysAsync(SayisalLotoParameters sayisalLotoParameters, bool trackChanges);
        Task<SayisalLotoDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<SayisalLotoDto> CreateOneNumbersArrayAsync(SayisalLotoDtoForInsertion sayisalLotoDtoForInsertion);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task UpdateOneNumbersArrayAsync(int id, SayisalLotoDtoForUpdate sayisalLotoDtoForUpdate, bool trackChanges);
    }
}
