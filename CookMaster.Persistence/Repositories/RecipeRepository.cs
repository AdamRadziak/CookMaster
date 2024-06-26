﻿using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CookMaster.Persistence.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(CookMasterDbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Recipe> GenerateRecipeForUserMenu(int dayCount, int mealCount, double rate, double popularity, int prepareTime)
        {
            var query =     Entities.Include(e => e.Photos)
                             .Include(e => e.Products)
                             .Include(e => e.Steps)
                             .Where(e => e.MealCount >= mealCount)
                             .Where(e => e.Rate >= rate)
                             .Where(e => e.Popularity >= popularity)
                             .Where(e => e.PrepareTime <= prepareTime)
                             .Take(dayCount*3).ToList();
            return query;
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

        public  IQueryable<Recipe> GetFavouritiesByUser(int idUser)
        {
            var query =  Entities.Include(e => e.Photos)
                                       .Include(e => e.Products)
                                       .Include(e => e.Steps)
                                       .Where(e => e.IdUserNavigation.Id == idUser).AsQueryable();
            return query;
        }

        public ICollection<Recipe> GetRecipesByIdMenu(int IdMenu)
        {
            return Entities.Include(e => e.Photos)
                                       .Include(e => e.Products)
                                       .Include(e => e.Steps)
                                       .Where(e => e.IdMenu == IdMenu).ToList();
        }

        public async Task<bool> RecipeExistAsync(int id)
        {
            return await Entities.AnyAsync(e => e.Id == id);
        }
    }
}
