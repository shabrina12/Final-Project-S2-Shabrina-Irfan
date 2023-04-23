using System.Text.RegularExpressions;
using System;
using System.Linq;
using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Repository
{
    public class ProfilingRepository : GeneralRepository<Profiling, string, MyContext>, IProfilingRepository
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IUniversityRepository _universityRepository;
        //private readonly IEducationRepository _educationRepository;
        public ProfilingRepository(MyContext context) : base(context)
        {
        }
        //public ProfilingRepository(
        //    MyContext context,
        //    IEmployeeRepository employeeRepository,
        //    IUniversityRepository universityRepository,
        //    IEducationRepository educationRepository) : base(context)
        //{
        //    _employeeRepository = employeeRepository;
        //    _universityRepository = universityRepository;
        //    _educationRepository = educationRepository;
        //}

        // Profiling/WorkPeriod: Menampilkan Employee berdasarkan lama kerjanya diurutkan secara desc
        //public async Task<IEnumerable<ProfilingWorkPeriodVM>> GetWorkPeriod()
        //{
        //    var getProfiling = await GetAllAsync();
        //    var getEmployee = await _employeeRepository.GetAllAsync();
        //    var employeeEduUni = from e in getEmployee
        //                         where 
        //                         select new ProfilingWorkPeriodVM
        //                         {

        //                         };
        //                         //SELECT *, DATEDIFF(day, hiring_date, GETDATE()) AS work_period_days FROM Employees;

        //    return employeeEduUni;
        //}

        // Profiling/TotalByMajor: Menampilkan total employee berdasarkan masing-masing major & univnya, diurutkan dari total employee terbanyak
        //public async Task<IEnumerable<TotalByMajorVM>> TotalbyMajor()
        //{
        //    var getUniversities = await _universityRepository.GetAllAsync();
        //    var getEducations = await _educationRepository.GetAllAsync();
        //    var totalByMajor = from uni in getUniversities
        //                       join edu in getEducations on uni.Id equals edu.UniversityId
        //                       orderby 3 descending
        //                       select new TotalByMajorVM
        //                       {
        //                           Major = edu.Major,
        //                           UniversityName = uni.Name
        //                       };

        //    return totalByMajor;
        //}

        //Profiling/AvgGPA/{tahun}: Tampilkan employee dengan GPA diatas rata-rata yang direkrut pada tahun yang diinputkan berdasarkan jurusan dan universitas.
        //public async Task<IEnumerable<TotalByMajorVM>> EmployeeAvgGpa(string tahun)
        //{
        //    var getEmployees = await _employeeRepository.GetAllAsync();
        //    var getUniversities = await _universityRepository.GetAllAsync();
        //    var getEducations = await _educationRepository.GetAllAsync();
        //    var getProfilings = await GetAllAsync();
        //    var avgValue = getEducations.Average(emp => emp.Gpa);
        //    var totalByMajor = from e in getEmployees
        //                       join p in getProfilings on e.Nik equals p.EmployeeNik
        //                       join edu in getEducations on p.EducationId equals edu.Id
        //                       join uni in getUniversities on edu.UniversityId equals uni.Id
        //                       where edu.Gpa > avgValue
        //                       //where e.HiringDate.Year.ToString() == tahun && edu.Gpa > avgValue
        //                       select new TotalByMajorVM
        //                       {
        //                           Nik = e.Nik,
        //                           FirstName = e.FirstName,
        //                           LastName = e.LastName,
        //                           BirthDate = e.BirthDate,
        //                           Gender = e.Gender,
        //                           HiringDate = e.HiringDate,
        //                           Email = e.Email,
        //                           PhoneNumber = e.PhoneNumber,
        //                           Major = edu.Major,
        //                           Gpa = edu.Gpa,
        //                           UniversityName = uni.Name
        //                       };

        //    return totalByMajor;
        //}
    }
}
