using Microsoft.EntityFrameworkCore;
using MoneyBase.Support.Application.Interfaces;
using System.Linq.Expressions;


namespace MoneyBase.Support.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
       where TEntity : class
    {
        protected readonly MoneyBaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(MoneyBaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(TKey id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
