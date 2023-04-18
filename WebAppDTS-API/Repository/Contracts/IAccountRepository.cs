using WebAppDTS_API.Models;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account, string>
    {
        Task<int> RegisterAsync(RegisterVM registerVM);
        Task<bool> LoginAsync(LoginVM loginVM);
        Task<string> GetRoleName(string email);
    }
}
