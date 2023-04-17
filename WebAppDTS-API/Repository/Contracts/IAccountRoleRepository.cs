using WebAppDTS_API.Models;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole, int>
    {
        Task<IEnumerable<string>> GetRolesByNikAsync(string nik);
    }
}
