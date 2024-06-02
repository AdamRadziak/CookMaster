using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookMaster.Persistence.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(CookMasterDbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Photo> GetPhotosByIdRecipe(int IdRecipe)
        {
            return Entities.Where(e => e.IdRecipe == IdRecipe).ToList();
        }

        public async Task<bool> PhotoExistsAsync(string filename)
        {
            return await Entities.AnyAsync(e => e.FileName == filename && e.Data != null);
        }
    }
}
