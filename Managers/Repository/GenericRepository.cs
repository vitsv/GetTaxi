using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Globalization;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Entity;

namespace Managers.Repository
{
    /// <summary>
    /// Generic repository
    /// </summary>
    public class GenericRepository : IRepository
    {
        private readonly string _connectionStringName;
        private DbContext _objectContext;
        private IUnitOfWork unitOfWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(this.ObjectContext);
                }
                return unitOfWork;
            }
        }

        private DbContext ObjectContext
        {
            get
            {
                return this._objectContext;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        public GenericRepository()
            : this(string.Empty)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public GenericRepository(string connectionStringName)
        {
            this._connectionStringName = connectionStringName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository"/> class.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        public GenericRepository(DbContext objectContext)
        {
            if (objectContext == null)
                throw new ArgumentNullException("objectContext");
            this._objectContext = objectContext;
        }

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return _objectContext.Set<TEntity>().SingleOrDefault<TEntity>(criteria);
        }


        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _objectContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _objectContext.Set<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _objectContext.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            IEnumerable<TEntity> records = Find<TEntity>(criteria);

            foreach (TEntity record in records)
            {
                Delete<TEntity>(record);
            }
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _objectContext.Set<TEntity>().AsEnumerable();
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return _objectContext.Set<TEntity>().Where(criteria);
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return _objectContext.Set<TEntity>().Where(criteria).FirstOrDefault();
        }


        public int Count<TEntity>() where TEntity : class
        {
            return _objectContext.Set<TEntity>().Count();
        }

        public int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return _objectContext.Set<TEntity>().Count(criteria);
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            return _objectContext.Set<TEntity>().AsQueryable<TEntity>();
        }

    }
}
