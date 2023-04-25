using WebAppDTS_API.Models;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Repository.Contracts
{
    public interface IProfilingRepository : IGeneralRepository<Profiling, string>
    {
        //Task<IEnumerable<AverageGpa>> EmployeeAvgGpa(string tahun);
        //Task<IEnumerable<TotalEmployeeByMajor>> TotalByMajor();
        //Task<IEnumerable<WorkPeriodVM>> WorkPeriod();
    }
}
