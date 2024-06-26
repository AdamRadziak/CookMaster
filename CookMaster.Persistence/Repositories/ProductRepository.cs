﻿using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookMaster.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CookMasterDbContext dbContext) : base(dbContext) { }

        public ICollection<Product> GetProductsByIdRecipe(int IdRecipe)
        {
            return Entities.Where(e => e.IdRecipe == IdRecipe).ToList();
        }

        public async Task<bool> ProductEditAllowedAsync(string name)
        {
            return await Entities.AnyAsync(e => e.Name == name);
        }
    }
}
