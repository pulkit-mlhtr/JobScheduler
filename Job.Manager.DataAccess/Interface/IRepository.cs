using System;
using System.Linq;

namespace Job.Manager.DataAccess.Interface
{
    interface IRepository
    {
    }
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Returns all records of TEntity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// Create TEntity in the database
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
        /// <summary>
        /// Remove specific TEntity from the database
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// Return specific TEntity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TEntity Find(Guid Id);
        /// <summary>
        /// Update TEntity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        /// <summary>
        /// Save changes made to Data sets
        /// </summary>
        void SaveChanges();
    }
}
