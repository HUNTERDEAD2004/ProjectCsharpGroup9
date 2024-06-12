using API.IService;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Service
{
    public class Service<T> : IAllServices<T> where T : class
    {
        AppDbContext _dbContext;
        DbSet<T> _dbSet;
        public Service()
        {
            _dbContext = new AppDbContext();
        }
        public Service(DbSet<T> dbSet, AppDbContext context)
        {
            this._dbSet = dbSet;
            this._dbContext = context;
        }
        public bool Create(T obj)
        {
            try
            {
                _dbSet.Add(obj);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(dynamic id)
        {
            try
            {
                var del = _dbSet.Find(id);
                _dbSet.Remove(del);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T GetByID(dynamic id)
        {
            return _dbSet.Find(id);
        }

        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public bool Upate(T obj)
        {
            try
            {
                _dbSet.Update(obj);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
