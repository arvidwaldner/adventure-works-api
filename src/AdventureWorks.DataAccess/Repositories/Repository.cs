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
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T GetById(short id);
        Task<T> GetByIdAsync(short id);
        T Insert(T entity);
        Task<T> InsertAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(int id);
        Task DeleteAsync(int id);
        void Delete(short id);
        Task DeleteAsync(short id);
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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T GetById(short id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(short id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Insert(T entity)
        {
            var insertedEntity = _dbSet.Add(entity);
            _context.SaveChanges();
            return insertedEntity.Entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            var insertedEntity = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return insertedEntity.Entity;
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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

        public async Task DeleteAsync(int id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
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
             
        public async Task DeleteAsync(short id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
