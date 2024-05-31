using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;

namespace CookMaster.Persistence.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(CookMasterDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Recipe> GetAllAsync(int c)
        {
            var query = Entities.Include(e => e.Photos)
                                       .Include(e => e.Products)
                                       .Include(e => e.Steps).Take(c).AsQueryable();

            return query;
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {


            var query = await Entities.Include(e => e.Photos)
                                       .Include(e => e.Products)
                                       .Include(e => e.Steps)
                                       .FirstOrDefaultAsync(e => e.Id == id); 

            return query;
        }

        public  IQueryable<Recipe> GetFavouritiesByUser(string email)
        {
            var query =  Entities.Include(e => e.Photos)
                                       .Include(e => e.Products)
                                       .Include(e => e.Steps)
                                       .Where(e => e.IdUserNavigation.Email == email).AsQueryable();
            return query;
        }

        public async Task<bool> RecipeExistAsync(int id)
        {
            return await Entities.AnyAsync(e => e.Id == id);
        }
    }
}
