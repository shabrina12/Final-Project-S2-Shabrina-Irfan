using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Repository
{
    public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
    {
        public EmployeeRepository(MyContext context) : base(context) { }
        //public string GetFullName(string email)
        //{
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
