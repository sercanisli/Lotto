using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface ISuperLotoService
    {
        IEnumerable<SuperLotoDto> GetAllNumbersArrays(bool trackChanges);
        SuperLotoDto GetOneNumbersArrayById(int id, bool trackChanges);
        SuperLotoDto CreateOneNumbersArray(SuperLotoDtoForInsertion superLotoDto);
        void UpdateOneNumbersArray(int id, SuperLotoDtoForUpdate superLotoDto, bool trackChanges);
        void DeleteOneNumbersArray(int id, bool trackChanges);
        public IEnumerable<int> GetOnlyNumbers(bool trackChanges);
        public List<int> GetRondomNumbers();
    }
}
