using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IPhotoRepository : IGenericRepository<Photo>
    {
        Task<bool> PhotoExistsAsync(string filename);
        ICollection<Photo> GetPhotosByIdRecipe(int IdRecipe);
        

    }
}
