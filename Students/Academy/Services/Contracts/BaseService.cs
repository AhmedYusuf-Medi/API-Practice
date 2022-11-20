using Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Services.Contracts
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class
    {
        protected readonly AcademyContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public BaseService(AcademyContext academyContext)
        {
            dbContext = academyContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) =>
            await dbContext.AddAsync(entity);

        public IQueryable<TEntity> All() =>
            dbSet;

        public IQueryable<TEntity> AllAsNoTracking() =>
           dbSet.AsNoTracking();

        public void Delete(TEntity entity) =>
           dbSet.Remove(entity);

        public Task<int> SaveChangesAsync() =>
            dbContext.SaveChangesAsync();

        public void Update(TEntity entity)
        {
            var entry = dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
