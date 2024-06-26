﻿using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Persistance.SqlServer.Model;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Services.Interfaces
{
    public interface IUserMenuService : IBaseService<UserMenu>
    {
        Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> GetByIdAsync(int id);
        Task<(bool IsSuccess, IPagedList<GetSingleUserMenuDTO>? entityList, HttpStatusCode StatusCode, string ErrorMessage)> GetListAsyncForUser<TOut>(SieveModel paginationParams, int IdUser);
        Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> GenerateUserMenuAsync(GenerateUserMenuDTO dto);
        Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateUserMenuAsync(AddUpdateUserMenuDTO dto, int id);
        Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeleteUserMenuAsync(int Id);
        Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeatachUserMenusFromUser(int idUser);


    }
}
