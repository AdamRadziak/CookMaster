using CookMaster.Persistence.Repositories.Interfaces;

namespace CookMaster.Persistence.UOW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRecipeRepository RecipeRepository { get; }
        IUserRepository UserRepository { get; }

        IProductRepository ProductRepository { get; }

        IStepRepository StepRepository { get; }

        IPhotoRepository PhotoRepository { get; }
        IGenericRepository<T> Repository<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        Task Rollback();
    }
}
