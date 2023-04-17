using WebAppDTS_API.Models;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University, int>
    {
        Task<University?> GetByNameAsync(string name);
        Task<bool> IsNameExistAsync(string name);
    }
}
