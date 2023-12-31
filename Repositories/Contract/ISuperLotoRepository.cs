﻿using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Cantracts
{
    public interface ISuperLotoRepository : IRepositoryBase<SuperLoto>
    {
        Task<PagedList<SuperLoto>> GetAllNumbersArrayAsync(SuperLotoParameters superLotoParameters, bool trackChanges);
        Task<IEnumerable<SuperLoto>> GetAllNumbersArrayWithoutPaginationAsync(bool trackChanges);
        Task<SuperLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        void CreateOneNumbersArray(SuperLoto superLoto);
        void UpdateOneNubersArray(SuperLoto superLoto);
        void DeleteOneNumbersArray(SuperLoto superLoto);
    }
}
