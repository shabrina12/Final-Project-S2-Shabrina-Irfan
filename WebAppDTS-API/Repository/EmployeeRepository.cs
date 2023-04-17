using Microsoft.EntityFrameworkCore;
using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Repository
{
    public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
    {
        public EmployeeRepository(MyContext context) : base(context) { }
        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            return employee is null ? string.Empty : string.Concat(employee.FirstName, " ", employee.LastName);
        }

        //public async Task<string> GetFullName(string email)
        //{
        //    using var transaction = _context.Database.BeginTransaction();
        //    var employee = _context.Employees.First(e => e.Email == email);
        //    //return employee.FirstName + " " + employee.LastName;
        //    if (employee == null)
        //    {
        //        return String.Empty;
        //    }
        //    else
        //    {
        //        return employee.FirstName + " " + employee.LastName;
        //    }
        //}
    }
}
