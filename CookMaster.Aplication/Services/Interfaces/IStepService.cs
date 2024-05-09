using CookMaster.Aplication.DTOs;
using CookMaster.Persistance.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Services.Interfaces
{
    public interface IStepService : IBaseService<Step>
    {
        Task<(bool IsSuccess, Step? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewStepAsync(AddUpdateStepDTO dto);

        Task<(bool IsSuccess, Step? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateStepAsync(AddUpdateStepDTO dto, int id);
    }
}
