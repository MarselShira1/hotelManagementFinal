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
       
        IEnumerable<User> GetAllUsers();

        //User GetUserById(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();


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
        public User GetUserById(int userId)
        {
            return _userRepository.GetById(userId); 
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

    }


}
