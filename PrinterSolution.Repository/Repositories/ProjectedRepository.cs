using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrinterSolution.Common.DTO;
using PrinterSolution.Repository.Database;
using PrinterSolution.Repository.Interfaces;
using System.Linq.Expressions;

namespace PrinterSolution.Repository.Repositories
{
    public class ProjectedRepository<TEntity, TModel> : IProjectedRepository<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseDTO
    {
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;
        private readonly DbSet<TEntity> dbSet;

        public ProjectedRepository(DatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            dbSet = dbContext.Set<TEntity>();
        }

        public bool Delete(TModel model)
        {
            dbSet.Remove(mapper.Map<TEntity>(model));
            dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteAsync(TModel model)
        {
            dbSet.Remove(mapper.Map<TEntity>(model));
            await dbContext.SaveChangesAsync();

            return true;
        }

        public TModel? FirstOrDefault(Expression<Func<TModel, bool>>? predicate = null)
        {
            return mapper.ProjectTo<TModel>(dbSet).FirstOrDefault();
        }

        public async Task<TModel?> FirstOrDefaultAsync(Expression<Func<TModel, bool>>? predicate = null)
        {
            return await mapper.ProjectTo<TModel>(dbSet).FirstOrDefaultAsync();
        }

        public TModel Insert(TModel entity)
        {
            dbSet.Add(mapper.Map<TEntity>(entity));
            dbContext.SaveChanges();

            return entity;
        }

        public async Task<TModel> InsertAsync(TModel entity)
        {
            dbSet.Add(mapper.Map<TEntity>(entity));
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public IEnumerable<TModel> Where(Expression<Func<TModel, bool>> predicate)
        {
            return dbSet.ProjectTo<TModel>(mapper.ConfigurationProvider).AsNoTracking().Where(predicate);
        }

        public async Task<IEnumerable<TModel>> WhereAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await dbSet.ProjectTo<TModel>(mapper.ConfigurationProvider).AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
