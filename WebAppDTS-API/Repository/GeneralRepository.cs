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
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }


        // GET BY ID
        public async Task<TEntity?> GetByIdAsync(TKey key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }

        // INSERT
        public virtual async Task<TEntity?> InsertAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // UPDATE
        public async Task<int> UpdateAsync(TEntity entity)
        {

            _context.Set<TEntity>().Update(entity);
            return await _context.SaveChangesAsync();            
            //_context.Entry(entity).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task<int> DeleteAsync(TKey key)
        {
            var entity = await GetByIdAsync(key);
            _context.Set<TEntity>().Remove(entity!);
            return await _context.SaveChangesAsync();           
        }
        public virtual async Task<bool> IsExist(TKey key)
        {
            var entity = await GetByIdAsync(key);
            return entity != null;
        }
    }
}
