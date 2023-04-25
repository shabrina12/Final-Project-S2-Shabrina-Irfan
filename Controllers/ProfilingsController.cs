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
            var result = await _repository.GetAvgGpaByYear(year);
            ResultFormat resultFormat = new ResultFormat
            {
                Data = result
            };
            return Ok(resultFormat);
        }

        [HttpGet("TotalByMajor")]
        public async Task<ActionResult> GetTotalByMajor()
        {
            var result = await _repository.GetTotalbyMajor();
            ResultFormat resultFormat = new ResultFormat
            {
                Data = result
            };
            return Ok(resultFormat);
        }

        [HttpGet("WorkPeriod")]
        public async Task<ActionResult> GetByWorkPeriod()
        {
            var result = await _repository.GetWorkPeriod();
            ResultFormat resultFormat = new ResultFormat
            {
                Data = result
            };
            return Ok(resultFormat);
        }
    }
}
