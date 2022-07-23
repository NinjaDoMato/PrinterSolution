using PrinterSolution.Repository.DTO;
using PrinterSolution.Repository.Entities;
using System.Linq.Expressions;

namespace PrinterSolution.Repository.Interfaces
{
    internal interface IRepository<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseDTO
    {
        TModel? FirsOrDefault(Expression<Func<TModel, bool>>? predicate = null);
        Task<TModel?> FirsOrDefaultAsync(Expression<Func<TModel, bool>>? predicate = null);

        IEnumerable<TModel> Where(Expression<Func<TModel, bool>> predicate);

        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
