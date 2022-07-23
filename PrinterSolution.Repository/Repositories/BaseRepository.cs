using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrinterSolution.Repository.Database;
using PrinterSolution.Repository.DTO;
using PrinterSolution.Repository.Entities;
using PrinterSolution.Repository.Interfaces;
using System.Linq.Expressions;

namespace PrinterSolution.Repository.Repositories
{
    internal class BaseRepository<TEntity, TModel> : IRepository<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseDTO
    {
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(DatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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

        public TModel? FirsOrDefault(Expression<Func<TModel, bool>>? predicate = null)
        {
            return mapper.ProjectTo<TModel>(dbSet).FirstOrDefault();
        }

        public async Task<TModel?> FirsOrDefaultAsync(Expression<Func<TModel, bool>>? predicate = null)
        {
            return await mapper.ProjectTo<TModel>(dbSet).FirstOrDefaultAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();

            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            dbSet.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            dbSet.Update(entity);
            dbContext.SaveChanges();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public IEnumerable<TModel> Where(Expression<Func<TModel, bool>> predicate)
        {
            return dbSet.ProjectTo<TModel>(mapper.ConfigurationProvider).AsNoTracking().Where(predicate);
        }
    }
}
