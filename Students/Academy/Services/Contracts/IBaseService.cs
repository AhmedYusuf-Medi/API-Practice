namespace Services.Contracts
{
    public interface IBaseService<TEntity>
        where TEntity : class
    {
        public IQueryable<TEntity> All();
        public IQueryable<TEntity> AllAsNoTracking();
        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
