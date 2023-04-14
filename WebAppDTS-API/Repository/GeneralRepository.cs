using WebAppDTS_API.Contexts;
using WebAppDTS_API.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace WebAppDTS_API.Repository
{
    public class GeneralRepository<TEntity, TKey, TContext> : IGeneralRepository<TEntity, TKey>
        where TEntity : class
        where TContext : MyContext
    {
        protected TContext _context;
        public GeneralRepository(TContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }


        // GET BY ID
        public async Task<TEntity?> GetById(TKey key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }

        // INSERT
        public async Task<int> Insert(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        // UPDATE
        public async Task<int> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task<int> Delete(TKey key)
        {
            var entity = await GetById(key);
            if (entity == null)
            {
                return 0;
            }
            _context.Entry(entity).State = EntityState.Deleted;
            return await _context.SaveChangesAsync();
        }
    }
}
