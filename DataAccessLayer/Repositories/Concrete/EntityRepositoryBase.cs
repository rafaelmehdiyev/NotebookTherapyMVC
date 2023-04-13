namespace DataAccessLayer.Repositories.Concrete
{
    public class EntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity> 
        where TEntity : class,ITable, new()
        where TContext : IdentityDbContext<AppUser>
    {
        private readonly TContext _context;
        public EntityRepositoryBase(TContext context)
        {
            _context = context;
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            return exp is null
                ? await GetQuery(includes).AsNoTracking().ToListAsync()
                : await GetQuery(includes).Where(exp).AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllPaginateAsync(int page, int size, Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            return exp is null
                ? await GetQuery(includes).Skip((page-1)*size).Take(size).AsNoTracking().ToListAsync()
                : await GetQuery(includes).Skip((page - 1) * size).Take(size).Where(exp).AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            return await GetQuery(includes).Where(exp).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> isExistAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await _context.Set<TEntity>().AnyAsync(exp);
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.ChangeTracker.Clear();
            _context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        #region Private Methods
        private IQueryable<TEntity> GetQuery(string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes is not null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
        #endregion
    }
}
