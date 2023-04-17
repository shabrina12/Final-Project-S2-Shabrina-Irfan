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
        public async Task Insert(TEntity entity)
        {
            _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // UPDATE
        public async Task Update(TEntity entity)
        {

            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            //_context.Entry(entity).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task Delete(TKey key)
        {
            var entity = await GetById(key);
            _context.Set<TEntity>().Remove(entity!);
            await _context.SaveChangesAsync();           
        }
        public virtual async Task<bool> IsExist(TKey key)
        {
            var entity = await GetById(key);
            return entity != null;
        }
    }
}
