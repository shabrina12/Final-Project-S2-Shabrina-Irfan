using Microsoft.EntityFrameworkCore;
using Web_API.Contexts;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<Profiling, int, SQLServerContext>, IProfilingRepository
    {
        public ProfilingRepository(SQLServerContext context) : base(context) { }

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
    }
}
