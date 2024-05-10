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
    public interface IPhotoService : IBaseService<Photo>
    {
        Task<(bool IsSuccess, Photo? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewPhotoAsync(AddUpdatePhotoDTO dto);

        Task<(bool IsSuccess, Photo? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdatePhotoAsync(AddUpdatePhotoDTO dto, int id);
    }
}
