using Web_API.DataModels;
using Web_API.Models;

namespace Web_API.Repository.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, string>
    {
        Task<IEnumerable<Employee>> GetAllWithRelationAsync();
        Task<UserDataDM> GetUserDataByEmailAsync(string email);

        Task<IEnumerable<Employee>> GetAllProfileAsync();
    }
}
