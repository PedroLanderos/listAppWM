﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Application.Responses;
using UserApi.Domain.Entities;

namespace UserApi.Application.Interfaces
{
    public interface UserInterface
    {
        //create a new user
        Task<ApiResponse> AddUser(UserEntity user);
        //delete an user
        Task<ApiResponse> DeleteUser(int userId);
        //update an user
        Task<ApiResponse> UpdateUser(UserEntity user);
        //find user by id
        Task<UserEntity> GetUserById(int userId);
        //get all the users
        Task<IEnumerable<UserEntity>> GetAllUsers();
    }
}
