using WebAppDTS_API.Models;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account, string>
    {
        Task<int> RegisterAsync(RegisterVM registerVM);
        Task<bool> LoginAsync(LoginVM loginVM);
        //Task<Account?> GetAccountByEmail(string email);

        //Task<string> GetRoleName(string email);
        //Task<UserVM> GetUserData(string email);
        //Task<int> UpdateToken(string email, string refreshToken, DateTime expiryTime);
        //Task<int> UpdateToken(string email, string refreshToken);
    }
}
