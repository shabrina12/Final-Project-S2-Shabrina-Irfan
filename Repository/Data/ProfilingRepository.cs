using Web_API.Contexts;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<Profiling, int, SQLServerContext>, IProfilingRepository
    {
        public ProfilingRepository(SQLServerContext context) : base(context) { }
    }
}
