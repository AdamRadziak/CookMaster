using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CookMasterDbContext dbContext) : base(dbContext) { }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
