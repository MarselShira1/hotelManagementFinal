using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using hotelManagement.Domain.Models;
using HotelManagementFinal.DAL.Persistence.Repositories;

namespace HotelManagementFinal.BLL.Services
{
    public interface IUserService
    {
       
        IEnumerable<User> GetAllUsers();

        UserModel GetUserById(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();


        public string GetUserEmailById(int userId);

        //   bool UpdateUserPassword(User user);
        bool UpdatePassword(int userId, string oldPass, string newPass);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(User user)
        {
            _userRepository.Add(user);
            _userRepository.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        //Marsel
        //14/02/2025
        public UserModel GetUserById(int userId)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
                return null;  

            return new UserModel
            {
                Id = user.Id,
                Emer = user.Emer,
                Mbiemer = user.Mbiemer,
                Email = user.Email,
                Role = user.Role,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                Invalidated = user.Invalidated.HasValue && user.Invalidated.Value == 1
            };
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }


        public string GetUserEmailById(int userId)
        {
            return _userRepository.GetEmailById(userId);
        }



        public bool UpdatePassword(int userId, string oldPass, string newPass)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return false; 

            // nese i vjetri esht ok
            if (!BCrypt.Net.BCrypt.Verify(oldPass, user.Password))
            {
                return false; 
            }

            // i riu dhe ruajtja
            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPass);
            user.Password = hashedNewPassword;

            _userRepository.Update(user);
            _userRepository.SaveChanges();

            return true;
        }

      
    }

}
