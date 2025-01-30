using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using HotelManagementFinal.DAL.Persistence.Repositories;
using HotelManagementFinal.Domain.Models;

namespace HotelManagementFinal.BLL.Services
{
    public interface IAuthService
    {
        void Register(User user);
        User Login(string email, string password);
        AuthModel emailExist(string email);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(User user)
        {
            
            if (_userRepository.GetByEmail(user.Email) != null)
            {
                throw new Exception("User with this email already exists.");
            }

            
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            
            _userRepository.Add(user);
            _userRepository.SaveChanges();
        }


        public User Login(string email, string password)
        {
            
            var user = _userRepository.GetByEmail(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user; 
        }

        public AuthModel emailExist(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null)
            {
                return new AuthModel
                {
                    Id = user.Id,
                    Email = user.Email,
                   Password= user.Password,
                   Role = user.Role,
                   
                };
            }
            return null;
        }

    }

}
