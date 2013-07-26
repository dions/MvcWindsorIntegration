using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using MvcWindsorIntegration.Classes.Interfaces;

namespace MvcWindsorIntegration.Classes.Repository
{
    public class RepositoryBase<T> : IRepository<T>, IDisposable
        where T : class
    {
        private DbContext _context;
        
        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public virtual IDbSet<T> DbSet
        {
            get { return _context.Set<T>(); }
        }

        #region IRepository Members

        public virtual DbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public virtual bool HasChanges
        {
            get { return Context.ChangeTracker.Entries().Any(); }
        }

        public virtual T Add(T entity)
        {
            return DbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual IList<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return DbSet.Where(whereCondition).ToList();
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            return DbSet.Where(whereCondition).FirstOrDefault();
        }

        public virtual T Get(Expression<Func<T, bool>> whereCondition)
        {
            return GetFirstOrDefault(whereCondition);
        }

        public virtual void Attach(T entity)
        {
            DbSet.Attach(entity);
        }

        public virtual bool Exists(T entity)
        {
            return DbSet.Local.Any(e => e == entity);
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return DbSet.AsQueryable();
        }

        public virtual long Count()
        {
            return DbSet.LongCount();
        }

        public virtual long Count(Expression<Func<T, bool>> whereCondition)
        {
            return DbSet.Where(whereCondition).LongCount();
        }

        public virtual bool SaveChanges()
        {
            try
            {
                ((IObjectContextAdapter)_context).ObjectContext.CommandTimeout = 1800;
                return 0 < _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var sb = new System.Text.StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new ApplicationException(sb.ToString());
            }
            
        }

        public virtual IQueryable<T> GetIncluding(string[] includes)
        {
            IQueryable<T> q = null;
            includes.ToList().ForEach(x => q = q.Include(x));
            return q;
        }

        public virtual IQueryable<T> GetIncluding(string[] includes, Expression<Func<T, bool>> whereCondition)
        {
            var q = DbSet.AsQueryable().Where(whereCondition);
            includes.ToList().ForEach(x => q = q.Include(x));
            return q;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        #endregion

    }
}
