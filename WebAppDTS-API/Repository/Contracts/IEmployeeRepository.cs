using WebAppDTS_API.Models;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, string>
    {
        //string GetFullName(string email);
    }
}
