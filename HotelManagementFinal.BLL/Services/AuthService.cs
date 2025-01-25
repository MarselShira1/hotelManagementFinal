using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.DAL.Persistence.Repositories;

namespace HotelManagementFinal.BLL.Services
{
    public interface IAuthService
    {
        void Register(User user);
        User Login(string email, string password);
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
                throw new Exception("Invalid email or password.");
            }

            return user; 
        }

    }

}
