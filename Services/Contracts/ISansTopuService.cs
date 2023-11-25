using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface ISansTopuService
    {
        IEnumerable<SansTopuDto> GetAllNumbersArrays(bool trackChanges);
        SansTopuDto GetOneNumbersArrayById(int id, bool trackChanges);
        SansTopuDto CreateOneNumbersArray(SansTopuDtoForInsertion sansTopuDtoForInsertion);
        void UpdateOneNumbersArray(int id, SansTopuDtoForUpdate sansTopuDtoForUpdate, bool trackChanges);
        void DeleteOneNumbersArray(int id, bool trackChanges);
    }
}
