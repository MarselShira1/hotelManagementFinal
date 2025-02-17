using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using hotelManagement.Domain.Models;
using iText.Commons.Actions.Contexts;
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
        public List<UserReservations> getRezervationCount();


    
        void Update(User user);
    }

    internal class UserRepository : _BaseRepository<User , int> , IUserRepository
       
    { private readonly HotelManagementContext _dbContext;

        public UserRepository(HotelManagementContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
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
            return _dbSet.Where(u => u.Invalidated == 1).ToList();
        }
        public string GetEmailById(int userId)
        {
            return _dbSet
                .Where(u => u.Id == userId)
                .Select(u => u.Email)
                .FirstOrDefault(); 
        }

        public List<UserReservations> getRezervationCount()
        {
            var userReservations = from user in _dbContext.Users
                                   join booking in _dbContext.Rezervime
                                   on user.Id equals booking.User into userBookings
                                   where user.Invalidated==1
                                   select new UserReservations
                                   {
                                       UserId = user.Id,
                                       Name = user.Emer,
                                       Surname = user.Mbiemer,
                                       Email = user.Email,
                                       ReservationCount = userBookings.Count()
                                   };

            return userReservations.ToList();
        }

        public void Update(User user)
        {
            _dbSet.Update(user);
            _dbContext.SaveChanges();
        }

    }



}
