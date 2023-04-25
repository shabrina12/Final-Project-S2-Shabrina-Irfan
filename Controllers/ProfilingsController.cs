using Microsoft.AspNetCore.Mvc;
using Web_API.Base;
using Web_API.DataModels;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingsController : GeneralController<IProfilingRepository, Profiling, int>
    {
        public ProfilingsController(IProfilingRepository repository) : base(repository) { }

        [HttpGet("AvgGPA/{year}")]
        public async Task<ActionResult> GetAvgGpa(int year)
        {
            ResultFormat resultFormat = new ResultFormat
            {
                Data = year
            };
            return Ok(resultFormat);
        }

        [HttpGet("TotalByMajor")]
        public async Task<ActionResult> GetTotalByMajor()
        {
            ResultFormat resultFormat = new ResultFormat
            {
                Data = ""
            };
            return Ok(resultFormat);
        }

        [HttpGet("WorkPeriod")]
        public async Task<ActionResult> GetByWorkPeriod()
        {
            ResultFormat resultFormat = new ResultFormat
            {
                Data = ""
            };
            return Ok(resultFormat);
        }
    }
}
