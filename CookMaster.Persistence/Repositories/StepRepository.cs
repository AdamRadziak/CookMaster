﻿using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookMaster.Persistence.Repositories
{
    public class StepRepository : GenericRepository<Step>, IStepRepository
    {
        public StepRepository(CookMasterDbContext dbContext) : base(dbContext) { }

        public ICollection<Step> GetStepsByIdRecipe(int IdRecipe)
        {
            return Entities.Where(e => e.IdRecipe == IdRecipe).ToList();
        }

        public async Task<bool> StepExistAsync(int id)
        {
            return await Entities.AnyAsync(e => e.Id == id);
        }
    }
}
