using Job.Manager.DataAccess.Database;
using Job.Manager.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Job.Manager.DataAccess.Common
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private JobsContext _dbContext;
        DbSet<TEntity> _dbSet;

        public Repository(JobsContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            lock (_dbSet)
            {
                return _dbSet.AsNoTracking();
            }
        }
        public void Add(TEntity entity)
        {
            _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public void SaveChanges() => _dbContext.SaveChanges();

        public TEntity Find(Guid Id)
        {
            return _dbSet.Find(Id);
        }
    }
}
