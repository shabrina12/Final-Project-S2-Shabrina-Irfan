﻿using WebAppDTS_API.Models;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, string>
    {
        Task<string> GetFullNameByEmailAsync(string email);
        Task<UserVM> GetUserDataByEmailAsync(string email);
        Task<IEnumerable<EmployeeMasterVM>> GetEmployeeEduUniv();
        Task<IEnumerable<AverageGpa>> EmployeeAvgGpa(string tahun);
        Task<IEnumerable<TotalEmployeeByMajor>> TotalByMajor();
        Task<IEnumerable<WorkPeriodVM>> WorkPeriod();
    }
}
