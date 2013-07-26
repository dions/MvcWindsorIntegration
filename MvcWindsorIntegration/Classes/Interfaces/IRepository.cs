using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MvcWindsorIntegration.Classes.Interfaces
{
    public interface IRepository<T> where T : class
    {
        DbContext Context { get; set; }

        bool HasChanges { get; }

        /// <summary>
        /// Get a selected extiry by the object primary key ID
        /// </summary>
        /// <param name="whereCondition"></param>
        T GetFirstOrDefault(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Short-hand call to GetFirstOrDefault
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> whereCondition);

        /// <summary> 
        /// Add entity to the repository 
        /// </summary> 
        /// <param name="entity">the entity to add</param> 
        /// <returns>The added entity</returns> 
        T Add(T entity);

        /// <summary> 
        /// Mark entity to be deleted within the repository 
        /// </summary> 
        /// <param name="entity">The entity to delete</param> 
        void Delete(T entity);

        /// <summary> 
        /// Updates entity within the the repository 
        /// </summary> 
        /// <param name="entity">the entity to update</param> 
        /// <returns>The updates entity</returns> 
        void Attach(T entity);

        bool Exists(T entity);

        /// <summary> 
        /// Load the entities using a linq expression filter
        /// </summary> 
        /// <param name="whereCondition">where condition</param> 
        /// <returns>the loaded entity</returns> 
        IList<T> GetAll(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary> 
        /// Query entities from the repository that match the linq expression selection criteria
        /// </summary> 
        /// <returns>the loaded entity</returns> 
        IQueryable<T> AsQueryable();

        /// <summary>
        /// Count using a filer
        /// </summary>
        long Count(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// All item count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long Count();

        /// <summary>
        /// Save any changes to the TContext
        /// </summary>
        bool SaveChanges();

        /// <summary>
        /// Works for including child tables for the current aggregate root
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paths"></param>
        /// <returns></returns>
        IQueryable<T> GetIncluding(string[] paths) ;

        /// <summary>
        /// works for including child tables for the current aggregate root with where condition.
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        IQueryable<T> GetIncluding(string[] paths, Expression<Func<T, bool>> whereCondition);
    }
}
