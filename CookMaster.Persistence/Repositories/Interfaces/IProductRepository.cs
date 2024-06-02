using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<bool> ProductEditAllowedAsync(string name);
        ICollection<Product> GetProductsByIdRecipe(int IdRecipe);


    }
}
