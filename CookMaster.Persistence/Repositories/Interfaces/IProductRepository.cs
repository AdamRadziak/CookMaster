using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<bool> ProductExistAsync(string name);
    }
}
