using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface ISuperLotoService
    {
        IEnumerable<SuperLotoDto> GetAllNumbersArrays(bool trackChanges);
        SuperLoto GetOneNumbersArrayById(int id, bool trackChanges);
        SuperLoto CreateOneNumbersArray(SuperLoto superLoto);
        void UpdateOneNumbersArray(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges);
        void DeleteOneNumbersArray(int id, bool trackChanges);

    }
}
