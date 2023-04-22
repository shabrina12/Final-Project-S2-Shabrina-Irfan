using Microsoft.EntityFrameworkCore;
using System;
using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Repository
{
    public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IProfilingRepository _profilingRepository;
        public EmployeeRepository(MyContext context,
            IUniversityRepository universityRepository,
            IEducationRepository educationRepository,
            IProfilingRepository profilingRepository) 
            : base(context) 
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _profilingRepository = profilingRepository;
        }
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

        // GET EMPLOYEE DATA ALONG WITH EDUCATION & UNIVERSITY
        public async Task<IEnumerable<EmployeeMaster>> GetEmployeeEduUniv()
        {
            var getProfiling = await _profilingRepository.GetAllAsync();
            var getEducation = await _educationRepository.GetAllAsync();
            var getUniversity = await _universityRepository.GetAllAsync();
            var getEmployee = await GetAllAsync();
            var employeeEduUni = from e in getEmployee
                              join p in getProfiling on e.Nik equals p.EmployeeNik
                              join edu in getEducation on p.EducationId equals edu.Id
                              join uni in getUniversity on edu.UniversityId equals uni.Id
                              select new EmployeeMaster {
                                  Nik = e.Nik,
                                  FirstName = e.FirstName,
                                  LastName = e.LastName,
                                  BirthDate = e.BirthDate,
                                  Gender = e.Gender,
                                  HiringDate = e.HiringDate,
                                  Email = e.Email,
                                  PhoneNumber = e.PhoneNumber,
                                  Major = edu.Major, 
                                  Degree = edu.Degree,
                                  GPA = edu.Gpa,
                                  UniversityName = uni.Name 
                              };
            
            return employeeEduUni;
        }
    }
}
