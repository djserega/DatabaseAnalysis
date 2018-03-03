using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.EntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }
   
        public TEntity GetFirstOrDefault<TResult>(Expression<Func<TEntity, bool>> predicate = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public TEntity Find(params object[] keyValues) 
            => _dbSet.Find(keyValues);

        public Task<TEntity> FindAsync(params object[] keyValues) 
            => _dbSet.FindAsync(keyValues);

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet.Count();
            }
            else
            {
                return _dbSet.Count(predicate);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Insert(TEntity entity)
        {
            var entry = _dbSet.Add(entity);
        }

        public void Insert(params TEntity[] entities) => _dbSet.AddRange(entities);

        public void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public void Delete(object id, string primaryKeyName)
        {
            // using a stub entity to mark for deletion
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var property = typeInfo.GetProperty(primaryKeyName);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
        }

        public void Delete(params TEntity[] entities) => _dbSet.RemoveRange(entities);

        public void Delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

        public void Update(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(object id)
        {
            _dbSet.Remove(Find(id));
        }
    }
}
