using AdventureWorks.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetById(short id);
        T Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(short id);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AdventureWorks2022Context _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AdventureWorks2022Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetById(short id)
        {
            return _dbSet.Find(id);
        }

        public T Insert(T entity)
        {
            var insertedEntity = _dbSet.Add(entity);
            _context.SaveChanges();
            return insertedEntity.Entity;
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(short id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
