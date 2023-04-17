using WebAppDTS_API.Models;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University, int>
    {
        Task<bool> IsNameExist(string name);
    }
}
