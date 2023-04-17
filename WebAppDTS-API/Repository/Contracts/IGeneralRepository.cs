namespace WebAppDTS_API.Repository.Contracts
{
    public interface IGeneralRepository<TEntity, TKey>
    {
        Task <IEnumerable<TEntity>> GetAll();
        Task <TEntity?> GetById(TKey key);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TKey key);
        Task<bool> IsExist(TKey key);
    }
}
