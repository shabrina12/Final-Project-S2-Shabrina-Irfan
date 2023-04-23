using WebAppDTS_API.Models;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IProfilingRepository : IGeneralRepository<Profiling, string>
    {
        //Task<IEnumerable<ProfilingWorkPeriodVM>> GetWorkPeriod();
        //Task<IEnumerable<TotalByMajorVM>> TotalbyMajor();
        //Task<IEnumerable<TotalByMajorVM>> EmployeeAvgGpa(string tahun);
    }
}
