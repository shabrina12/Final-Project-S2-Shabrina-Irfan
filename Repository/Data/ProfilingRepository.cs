using Microsoft.EntityFrameworkCore;
using Web_API.Contexts;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<Profiling, int, SQLServerContext>, IProfilingRepository
    {
        public ProfilingRepository(SQLServerContext context) : base(context) { }

        public async Task<dynamic> GetAvgGpaByYear(int year)
        {
            var query = from p in _context.Profilings
                        join em in _context.Employees
                            on p.EmployeeNik equals em.Nik
                        join e in _context.Educations
                            on p.EducationId equals e.Id
                        join u in _context.Universities
                            on e.UniversityId equals u.Id
                        where em.HiringDate.Year == year
                        group new { p, e, u, em } by new { e.Major, u.Name } into g
                        select new { Major = g.Key.Major, University = g.Key.Name, Year = year, Average = g.Average(x => x.e.Gpa) };
            return await query.ToListAsync();
        }

        public async Task<dynamic> GetTotalbyMajor()
        {
            var query = from p in _context.Profilings
                        join e in _context.Educations
                            on p.EducationId equals e.Id
                        join u in _context.Universities
                            on e.UniversityId equals u.Id
                        group new {p,e,u } by new {e.Major, u.Name} into g
                        orderby g.Count() 
                        descending
                        select new {Major = g.Key.Major, University = g.Key.Name, Total = g.Count()};
            return await query.ToListAsync();
        }

        public async Task<dynamic> GetWorkPeriod()
        {
            var query = from p in _context.Profilings
                        join e in _context.Employees
                            on p.EmployeeNik equals e.Nik
                        orderby e.HiringDate
                        select new { 
                            Name = (e.FirstName+" "+e.LastName),
                            NIK = e.Nik,
                            PeriodDays = ((int)(DateTime.Now - e.HiringDate).TotalDays)
                        };
            return await query.ToListAsync();
        }
    }
}
