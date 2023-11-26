﻿using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

namespace Repositories.EntityFrameworkCore
{
    public class SansTopuRepository : RepositoryBase<SansTopu>, ISansTopuRepository
    {
        public SansTopuRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneNumbersArray(SansTopu sansTopu) => Create(sansTopu);

        public void DeleteOneNumbersArray(SansTopu sansTopu) => Delete(sansTopu);

        public async Task<PagedList<SansTopu>> GetAllNumbersArrayAsync(SansTopuParameters sansTopuParameters, bool trackChanges)
        {
            var entities = await FindAll(trackChanges)
                .OrderBy(st => st.Date)
                .ToListAsync();

            return PagedList<SansTopu>.ToPagedList(entities, sansTopuParameters.PageNumber, sansTopuParameters.PageSize);
        }
           

        public async Task<SansTopu> GetOneNumbersArrayByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(st => st.Id == id, trackChanges).SingleOrDefaultAsync();

        public void UpdateOneNumbersArray(SansTopu sansTopu) => Update(sansTopu);
    }
}
