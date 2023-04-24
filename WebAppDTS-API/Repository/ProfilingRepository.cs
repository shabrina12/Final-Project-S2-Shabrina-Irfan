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
        //private readonly IUniversityRepository _universityRepository;
        //private readonly IEducationRepository _educationRepository;
        //private readonly IEmployeeRepository _employeeRepository;
        public ProfilingRepository(MyContext context) : base(context)
        {
        }

        //public ProfilingRepository(MyContext context,
        //    IUniversityRepository universityRepository,
        //    IEducationRepository educationRepository,
        //    IEmployeeRepository employeeRepository)
        //    : base(context)
        //{
        //    _universityRepository = universityRepository;
        //    _educationRepository = educationRepository;
        //    _employeeRepository = employeeRepository;
        //}
    }
}
