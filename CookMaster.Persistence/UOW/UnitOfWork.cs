using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistence.Repositories;
using CookMaster.Persistence.Repositories.Interfaces;
using CookMaster.Persistence.UOW.Interfaces;
using System.Collections;

namespace CookMaster.Persistence.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CookMasterDbContext _dbContext;
        private readonly Hashtable _repositories;
        private bool disposed;

        public IUserRepository UserRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public IStepRepository StepRepository { get; private set; }

        public IPhotoRepository PhotoRepository { get; private set; }
        public IRecipeRepository RecipeRepository { get; private set; }

        public UnitOfWork(CookMasterDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _repositories = new Hashtable();

            UserRepository = new UserRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
            StepRepository = new StepRepository(_dbContext);
            PhotoRepository = new PhotoRepository(_dbContext);
            RecipeRepository = new RecipeRepository(_dbContext);
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return _repositories[type] as IGenericRepository<T> ?? throw new InvalidOperationException($"Repository for {typeof(T).Name} is null.");
        }

        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}

