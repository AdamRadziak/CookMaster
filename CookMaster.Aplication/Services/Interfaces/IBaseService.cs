using CookMaster.Aplication.Helpers.PagedList;
using Sieve.Models;
using System.Net;

namespace CookMaster.Aplication.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        IBaseService<T> WithTracking();
        IBaseService<T> WithoutTracking();
        Task<(bool IsSuccess, T? entity, HttpStatusCode StatusCode, string ErrorMessage)> GetByIdAsync(params object?[]? id);
        Task<(bool IsSuccess, IPagedList<TOut>? entityList, HttpStatusCode StatusCode, string ErrorMessage)> SearchAsync<TOut>(SieveModel paginationParams, Func<T, TOut> formatterCallback);
        Task<(bool IsSuccess, T? entity, HttpStatusCode StatusCode, string ErrorMessage)> AddAsync(T entity);
        Task<(bool IsSuccess, T? entity, HttpStatusCode StatusCode, string ErrorMessage)> AddAndSaveAsync(T entity);
        Task<T> UpdateAsync(T entity, params object?[]? id);
        Task<(bool IsSuccess, T? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateAndSaveAsync(T entity, params object?[]? id);
        Task DeleteAsync(params object?[]? id);
        Task<(bool IsSuccess, T? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeleteAndSaveAsync(params object?[]? id);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
