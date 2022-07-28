using PrinterSolution.Common.DTO;
using System.Linq.Expressions;

namespace PrinterSolution.Repository.Interfaces
{
    /// <summary>
    /// Representation of the database for the given entity class, projecting it to the given DTO.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public interface IProjectedRepository<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseDTO
    {
        TModel? FirstOrDefault(Expression<Func<TModel, bool>>? predicate = null);
        Task<TModel?> FirstOrDefaultAsync(Expression<Func<TModel, bool>>? predicate = null);

        IEnumerable<TModel> Where(Expression<Func<TModel, bool>> predicate);
        Task<IEnumerable<TModel>> WhereAsync(Expression<Func<TModel, bool>> predicate);

        TModel Insert(TModel entity);
        Task<TModel> InsertAsync(TModel entity);
    }
}
