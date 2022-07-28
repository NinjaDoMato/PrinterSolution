using System.Linq.Expressions;

namespace PrinterSolution.Repository.Interfaces
{
    /// <summary>
    /// Representation of the database for the given entity class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null);

        TEntity Single(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>[]? includeProperties = null);

        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
