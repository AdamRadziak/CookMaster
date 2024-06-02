using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IStepRepository : IGenericRepository<Step>
    {
        Task<bool> StepExistAsync(int id);
        ICollection<Step> GetStepsByIdRecipe(int IdRecipe);
    }
}
