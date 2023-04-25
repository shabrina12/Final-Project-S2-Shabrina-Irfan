using Web_API.Models;

namespace Web_API.Repository.Contracts
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole, int>
    {
        Task<IEnumerable<string>> GetRolesByNikAsync(string nik);
    }
}
