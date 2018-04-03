using Bussiness.Models;
using DataAccsess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Mappers
{
    public static class UserMapper
    {
        public static User AsDto(this UserModel userModel)
        {
            if (userModel==null)
            {

                return null;
            }

            return new User
            {
                Id=userModel.Id,
                Username = userModel.Username,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                PasswordHash = userModel.PasswordHash
            };
        }

        public static UserModel AsModel(this User user)
        {
            if (user == null)
            {

                return null;
            }

            return new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash,
                IsAdmin = user.IsAdmin
            };
        }
    }
}