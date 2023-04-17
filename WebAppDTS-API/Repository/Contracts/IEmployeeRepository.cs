using WebAppDTS_API.Models;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, string>
    {
        //Task<string> GetFullName(string email);
        Task<string> GetFullNameByEmailAsync(string email);
    }
}
