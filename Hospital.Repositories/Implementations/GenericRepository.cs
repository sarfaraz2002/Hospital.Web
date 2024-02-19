using Hospital.Repositories.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Implementations
{
    public class GenericRepository<T> : IDisposable,IGenericRepository<T> where T : class
    {
        public readonly ApplicationDbContext _context;
  /*      internal DbSet<T> dbSet;*/

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if(_context.Entry(entity).State==EntityState.Detached) {
                _context.Set<T>().Attach(entity);
            }
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
               _context.Set<T>().Attach(entity);
            }
           _context.Set<T>().Remove(entity);
            return entity;
        }
        public bool disposed =false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query =_context.Set<T>();
            if(filter != null)
            {
                query = query.Where(filter);
            }
            foreach(var includeProperty  in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if(orderBy != null)
            {
                return  orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            var existingEntity = _context.Set<T>().Local.FirstOrDefault();
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
