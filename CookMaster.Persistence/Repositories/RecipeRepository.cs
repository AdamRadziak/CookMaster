using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;

namespace CookMaster.Persistence.Repositories
{
    public class RecipeRepository : GenericRepository<RecipesDetail>, IRecipeRepository
    {
        CookMasterDbContext _context;
        public RecipeRepository(CookMasterDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IList?> GetByNameAsync(string name)
        {


            var query = await (from recipe in _context.Set<Recipe>()
                        join recipeDetail in _context.Set<RecipesDetail>()
                            on recipe.Id equals recipeDetail.IdRecipe
                        join step in _context.Set<Step>()
                            on recipeDetail.IdStep equals step.Id
                        join product in _context.Set<Product>()
                            on recipeDetail.IdProduct equals product.Id
                        join photo in _context.Set<Photo>()
                        on recipeDetail.IdPhoto equals photo.Id
                        where recipe.Name == name
                        select new { recipe, step,product,photo }).ToListAsync();

            return query;
        }

        public async Task<bool> RecipeEditAllowedAsync(string name)
        {
            return await _context.Set<Recipe>().AnyAsync(e => e.Name == name);
        }
    }
}
