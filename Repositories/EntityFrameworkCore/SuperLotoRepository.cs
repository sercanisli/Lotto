﻿using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Cantracts;

namespace Repositories.EntityFrameworkCore
{
    public class SuperLotoRepository : RepositoryBase<SuperLoto>, ISuperLotoRepository
    {
        public SuperLotoRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneNumbersArray(SuperLoto superLoto) => Create(superLoto);

        public void DeleteOneNumbersArray(SuperLoto superLoto) => Delete(superLoto);

        public async Task<PagedList<SuperLoto>> GetAllNumbersArrayAsync(SuperLotoParameters superLotoParameters, bool trackChanges)
        {
            var entities = await FindAll(trackChanges).ToListAsync();
            return PagedList<SuperLoto>.ToPagedList(entities, superLotoParameters.PageNumber, superLotoParameters.PageSize);
        }

        public async Task<IEnumerable<SuperLoto>> GetAllNumbersArrayAsyncWithoutPaginationAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<SuperLoto> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(sl => sl.Id == id, trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNubersArray(SuperLoto superLoto) => Update(superLoto);
    }
}
