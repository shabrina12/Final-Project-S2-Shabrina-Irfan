using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Base;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingController : BaseController<IProfilingRepository, Profiling, string>
    {
        private readonly IProfilingRepository _repository;
        public ProfilingController(IProfilingRepository repository) : base(repository)
        {
            _repository = repository;
        }

        //[HttpGet("AvgGPA/{tahun}")]
        //public async Task<ActionResult> EmployeeAverageGpa(string tahun)
        //{
        //    try
        //    {
        //        var results = await _repository.EmployeeAvgGpa(tahun);
        //        return Ok(new
        //        {
        //            code = StatusCodes.Status200OK,
        //            status = HttpStatusCode.OK.ToString(),
        //            Message = results
        //        });
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
