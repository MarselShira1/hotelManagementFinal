using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence;
using hotelManagement.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
namespace hotelManagement.DAL.Persistence.Repositories
{
    public interface IUserRepository : _IBaseRepository<User , int>
    {
        //User Authenticate(string email, string password);
        //void Register(User user);
        //IEnumerable<Role> GetRoles();
        User GetByEmail(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        
        User GetById(int id);
        public string GetEmailById(int userId);


    }

    internal class UserRepository : _BaseRepository<User , int> , IUserRepository
    {

        public UserRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }

        public new User GetById(int id)
        {
            return base.GetById(id);
           // return _dbSet.FirstOrDefault(u => u.Id == id && u.Invalidated == 1);
        }

        public User GetByEmail(string email)
        {

            return _dbSet.Where(w => w.Email == email && w.Invalidated == 1).FirstOrDefault();

        }


        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbSet.Where(u => u.Invalidated == 1).ToListAsync();
        }



        public IEnumerable<User> GetAll()
        {
            return _dbSet.Where(u => u.Invalidated != 1).ToList();
        }
        public string GetEmailById(int userId)
        {
            return _dbSet
                .Where(u => u.Id == userId)
                .Select(u => u.Email)
                .FirstOrDefault(); 
        }

    }



}
