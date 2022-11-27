using AutoMapper;
using Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Services.Contracts
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class
    {
        protected readonly AcademyContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseService(AcademyContext academyContext, IMapper mapper)
        {
            _dbContext = academyContext ?? throw new ArgumentNullException();
            _mapper = mapper ?? throw new ArgumentNullException();
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) =>
            await _dbSet.AddAsync(entity);

        public IQueryable<TEntity> All() =>
            _dbSet;

        public IQueryable<TEntity> AllAsNoTracking() =>
           _dbSet.AsNoTracking();

        public void Delete(TEntity entity) =>
           _dbSet.Remove(entity);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _dbContext.SaveChangesAsync(cancellationToken);

        public void Update(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
