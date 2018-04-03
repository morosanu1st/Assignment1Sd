using Bussiness.Mappers;
using Bussiness.Models;
using DataAccsess.DTO;
using DataAccsess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Bussiness.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public UserModel GetUserById(int id)
        {
            return _userRepository.GetUserById(id)?.AsModel();
        }

        public List<UserModel> GetAllUsers()
        {
            return _userRepository.GetAllUsers().Select(user => user.AsModel()).ToList();
        }

        public UserModel GetUserByUserName(string userName)
        {
            return _userRepository.GetUserByUserName(userName)?.AsModel();
        }

        public bool CreatetUser(UserModel user)
        {
            if (user == null || _userRepository.GetUserByUserName(user.Username) != null)
            {

                return false;
            }

            _userRepository.InsertUser(user.AsDto());

            return true;
        }

        public string HashPassword(string password)
        {
            if(password==null)
            {

                return null;
            }
            SHA256 hasher = new SHA256Cng();
            var hash= (hasher.ComputeHash(Encoding.ASCII.GetBytes(password)));

            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }

        public bool UpdateUser(UserModel user)
        {
            var toUpdate = GetUserById(user.Id);
            if (user == null || toUpdate == null || _userRepository.GetUserByUserName(user.Username) != null)
            {

                return false;
            }

            user.Username = user.Username ?? toUpdate.Username;
            user.FirstName = user.FirstName ?? toUpdate.FirstName;
            user.LastName = user.LastName ?? toUpdate.LastName;
            user.PasswordHash = user.PasswordHash ?? toUpdate.PasswordHash;
            try
            {
                _userRepository.UpdateUser(user.AsDto());

            }
            catch (Exception e)
            {

                return false;
            }

            return true;
        }

        public bool DeleteUser(UserModel user)
        {
            var toDelete = GetUserById(user.Id);
            if (user == null || toDelete == null)
            {

                return false;
            }

            _userRepository.DeleteUser(toDelete.Id);

            return true;
        }
    }
}