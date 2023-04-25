using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;
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
        public async Task<IEnumerable<EmployeeMasterVM>> GetEmployeeEduUniv()
        {
            var getProfiling = await _profilingRepository.GetAllAsync();
            var getEducation = await _educationRepository.GetAllAsync();
            var getUniversity = await _universityRepository.GetAllAsync();
            var getEmployee = await GetAllAsync();
            var employeeEduUni = from e in getEmployee
                              join p in getProfiling on e.Nik equals p.EmployeeNik
                              join edu in getEducation on p.EducationId equals edu.Id
                              join uni in getUniversity on edu.UniversityId equals uni.Id
                              select new EmployeeMasterVM {
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

        //AvgGPA/{tahun}: Tampilkan employee dengan GPA diatas rata-rata yang direkrut pada tahun yang diinputkan berdasarkan jurusan dan universitas.
        public async Task<IEnumerable<AverageGpa>> EmployeeAvgGpa(string tahun)
        {
            var getEmployees = await GetAllAsync();
            var getUniversities = await _universityRepository.GetAllAsync();
            var getEducations = await _educationRepository.GetAllAsync();
            var getProfilings = await _profilingRepository.GetAllAsync();
            var avgValue = getEducations.Average(emp => emp.Gpa);
            var totalByMajor = from e in getEmployees
                               join p in getProfilings on e.Nik equals p.EmployeeNik
                               join edu in getEducations on p.EducationId equals edu.Id
                               join uni in getUniversities on edu.UniversityId equals uni.Id
                               where e.HiringDate.Year.ToString() == tahun && edu.Gpa > avgValue
                               orderby edu.Gpa descending
                               select new AverageGpa
                               {
                                   Nik = e.Nik,
                                   FirstName = e.FirstName,
                                   LastName = e.LastName,
                                   BirthDate = e.BirthDate,
                                   Gender = e.Gender,
                                   HiringDate = e.HiringDate,
                                   Email = e.Email,
                                   PhoneNumber = e.PhoneNumber,
                                   Major = edu.Major,
                                   Gpa = edu.Gpa,
                                   UniversityName = uni.Name
                               };

            return totalByMajor;
        }

        //TotalByMajor: Menampilkan total employee berdasarkan masing-masing major & univnya, diurutkan dari total employee terbanyak
        public async Task<IEnumerable<TotalEmployeeByMajor>> TotalByMajor()
        {
            var getUniversities = await _universityRepository.GetAllAsync();
            var getEducations = await _educationRepository.GetAllAsync();
            //var totalEmp = getEducations.Count();
            var totalByMajor = from uni in getUniversities
                               join edu in getEducations on uni.Id equals edu.UniversityId
                               group new { uni, edu } by new { uni.Name, edu.Major } into grp
                               orderby grp.Count() descending
                               select new TotalEmployeeByMajor
                               {
                                   Major = grp.Key.Major,
                                   UniversityName = grp.Key.Name,
                                   TotalEmployees = grp.Count()
                               };
            return totalByMajor;
        }

        //WorkPeriod: Menampilkan Employee berdasarkan lama kerjanya diurutkan secara desc
        public async Task<IEnumerable<WorkPeriodVM>> WorkPeriod()
        {
            var getEmployees = await GetAllAsync();
            DateTime endDate = DateTime.Now;
            var workPeriod = from e in getEmployees
                             let startDate = e.HiringDate
                             select new WorkPeriodVM
                             {
                                 Nik = e.Nik,
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 BirthDate = e.BirthDate,
                                 Gender = e.Gender,
                                 HiringDate = e.HiringDate,
                                 Email = e.Email,
                                 PhoneNumber = e.PhoneNumber,
                                 WorkPeriodInDays = (endDate - startDate).Days
                             };
            return workPeriod;
        }

    }
}
