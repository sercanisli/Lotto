﻿using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IOnNumaraService
    {
        Task<IEnumerable<OnNumaraDto>> GetAllNumbersArraysAsync(OnNumaraParameters onNumaraParameters, bool trackChanges);
        Task<OnNumaraDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges);
        Task<OnNumaraDto> CreateOneNumbersArrayAsync(OnNumaraDtoForInsertion onNumaraDtoForInsertion);
        Task UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task<List<int>> GetRondomNumbersAsync();
    }
}
