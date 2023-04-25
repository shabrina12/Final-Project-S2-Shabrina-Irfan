using Web_API.DataModels;
using Web_API.Models;

namespace Web_API.Repository.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account, string>
    {
        Task RegisterAsync(RegisterDM registerDM, string role);
        Task<bool> LoginAsync(LoginDM loginDM);
    }
}
