using Microsoft.EntityFrameworkCore;
using PrinterSolution.Repository.Database;
using PrinterSolution.Repository.Interfaces;
using System.Linq.Expressions;

namespace PrinterSolution.Repository.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DatabaseContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public bool Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null)
        {
            var query = ApplyQuery(predicate, includeProperties);

            return query.FirstOrDefault();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null)
        {
            var query = ApplyQuery(predicate, includeProperties);

            return await query.FirstOrDefaultAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            dbContext.ChangeTracker.Clear();

            dbSet.Add(entity);
            dbContext.SaveChanges();

            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            dbContext.ChangeTracker.Clear();

            dbSet.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public TEntity Single(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null)
        {
            var query = ApplyQuery(predicate, includeProperties);

            return query.Single();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null)
        {
            var query = ApplyQuery(predicate, includeProperties);

            return await query.SingleAsync();
        }

        public TEntity Update(TEntity entity)
        {
            dbContext.ChangeTracker.Clear();

            dbSet.Update(entity);
            dbContext.SaveChanges();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            dbContext.ChangeTracker.Clear();

            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }


        private IQueryable<TEntity> ApplyQuery(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = dbSet.Where(predicate);
            }

            return query;
        }
    }
}
