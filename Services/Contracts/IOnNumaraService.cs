﻿using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IOnNumaraService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllNumbersArraysAsync(LinkParameters<OnNumaraParameters> linkParameters, bool trackChanges);
        Task<OnNumaraDto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken = default);
        Task<OnNumaraDto> GetOneNumbersArrayByDateAsync(DateTime date, bool trackChanges);
        Task<OnNumaraDto> CreateOneNumbersArrayAsync(OnNumaraDtoForInsertion onNumaraDtoForInsertion);
        Task UpdateOneNumbersArrayAsync(int id, OnNumaraDtoForUpdate onNumaraDtoForUpdate, bool trackChanges);
        Task DeleteOneNumbersArrayAsync(int id, bool trackChanges);
        Task<MatchRateDto> CompareOnNumaraNumbersAsync(OnNumaraDtoForCompare onNumaraDtoForCompare);
        Task<MatchRateDto> CompareOnNumaraNumbersWithOnNumaraLogsNumbersAsync(OnNumaraDtoForCompareWithLogs onNumaraDtoForCompareWithLogs);
        Task<OnNumaraDtoForRandom> GetRondomNumbersAsync(string userName);
        Task<OnNumaraDtoForLastItem> GetLastItemAsync(bool trackChanges);
    }
}
