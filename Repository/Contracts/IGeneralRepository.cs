using Microsoft.AspNetCore.Mvc;

namespace Web_API.Repository.Contracts
{
    public interface IGeneralRepository<TEntity, TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey key);
        Task<TEntity?> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> IsExist(TKey key);
    }
}
