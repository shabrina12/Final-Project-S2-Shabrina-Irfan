using Web_API.Models;

namespace Web_API.Repository.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University, int>
    {
        Task<bool> IsNameExist(string name);
    }
}
