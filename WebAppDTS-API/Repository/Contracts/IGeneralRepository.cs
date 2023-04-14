namespace WebAppDTS_API.Repository.Contracts
{
    public interface IGeneralRepository<TEntity, TKey>
    {
        Task <IEnumerable<TEntity>> GetAll();
        Task <TEntity?> GetById(TKey key);
        Task<int> Insert(TEntity entity);
        Task<int> Update(TEntity entity);
        Task<int> Delete(TKey key);
    }
}
