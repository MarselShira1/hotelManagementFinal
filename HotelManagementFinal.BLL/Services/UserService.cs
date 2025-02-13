using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using HotelManagementFinal.DAL.Persistence.Repositories;

namespace HotelManagementFinal.BLL.Services
{
    public interface IUserService
    {
        //void RegisterUser(string username, string email, string password, int roleId);
        //User Authenticate(string email, string password);
        IEnumerable<User> GetAllUsers();
        public string GetUserEmailById(int userId);

        //void UpdateUserRole(int userId, int roleId);
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
        public string GetUserEmailById(int userId)
        {
            return _userRepository.GetEmailById(userId);
        }
    }

}
