﻿using Microsoft.EntityFrameworkCore;
using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;
using WebAppDTS_API.ViewModels;

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

        public async Task<UserVM> GetUserDataByEmailAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            return new UserVM
            {
                Nik = employee!.Nik,
                Email = employee.Email,
                FullName = string.Concat(employee.FirstName, " ", employee.LastName)
            };
        }
    }
}
