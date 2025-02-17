using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;


namespace hotelManagement.DAL.Persistence.Repositories
{
    internal class _BaseRepository
    {
    }
}

public interface _IBaseRepository<T, T1> where T : BaseEntity<T1>
{
    T GetById(T1 id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Delete(T entity);
    void SaveChanges();
    void SaveChangesAsync();
}

internal class _BaseRepository<T, T1> : _IBaseRepository<T, T1> where T : BaseEntity<T1>
{
    protected readonly HotelManagementContext _dbContext;
    protected readonly DbSet<T> _dbSet;
    public _BaseRepository(HotelManagementContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

  

    public IEnumerable<T> GetAll()
    {
        return _dbSet.AsNoTracking();  
    }



public T GetById(T1 id)
    {
        return _dbSet.Find(id);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async void SaveChangesAsync()
    {
       await _dbContext.SaveChangesAsync();
    }
    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

}

