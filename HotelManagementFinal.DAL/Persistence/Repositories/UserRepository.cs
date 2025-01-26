using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence;
using hotelManagement.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementFinal.DAL.Persistence.Repositories
{
    public interface IUserRepository : _IBaseRepository<User , int>
    {
        //User Authenticate(string email, string password);
        //void Register(User user);
        //IEnumerable<Role> GetRoles();
        User GetByEmail(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }

    internal class UserRepository : _BaseRepository<User , int> , IUserRepository
    {

        public UserRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }

        public new User GetById(int id)
        {
            return base.GetById(id);
        }

        public User GetByEmail(string email)
        {

            return _dbSet.Where(w => w.Email == email && w.Invalidated == 1).FirstOrDefault();

        }


        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return base.GetAll();
        }
    }


}
