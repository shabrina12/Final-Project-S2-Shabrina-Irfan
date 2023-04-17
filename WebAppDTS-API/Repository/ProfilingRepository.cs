using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Repository
{
    public class ProfilingRepository : GeneralRepository<Profiling, string, MyContext>, IProfilingRepository
    {
        public ProfilingRepository(MyContext context) : base(context) { }
    }
}
