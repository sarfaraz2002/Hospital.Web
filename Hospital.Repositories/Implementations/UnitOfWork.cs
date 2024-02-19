using Hospital.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        public readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool disposed = false;
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

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
           IGenericRepository<T> repository = new GenericRepository<T>(_context);
            return repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
