using Microsoft.EntityFrameworkCore;
using Web_API.Contexts;
using Web_API.DataModels;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string, SQLServerContext>, IEmployeeRepository
    {
        public EmployeeRepository(SQLServerContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetAllWithRelationAsync()
        {
            return await _context.Employees.Include(e => e.Account).ToListAsync();
        }

        public async Task<UserDataDM> GetUserDataByEmailAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            return new UserDataDM
            {
                Nik = employee!.Nik,
                Email = employee.Email,
                FullName = string.Concat(employee.FirstName, " ", employee.LastName)
            };
        }

        public async Task<IEnumerable<Employee>> GetAllProfileAsync()
        {
            return await _context.Employees.Include(e => e.Profiling)
                                           .ThenInclude(p => p.Education)
                                           .ThenInclude(e => e.University)
                                           .ToListAsync();
        }
    }
}
