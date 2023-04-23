using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Base;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository;
using WebAppDTS_API.Repository.Contracts;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<IEmployeeRepository, Employee, string>
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [HttpGet("Master")]
        public async Task<ActionResult> GetEmployeeEduUnivData()
        {
            try
            {
                var results = await _repository.GetEmployeeEduUniv();
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    Message = results
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("avg/{tahun}")]
        public async Task<ActionResult> GetEmployeesWithGpaAboveAvg(string tahun)
        {
            try
            {
                var results = await _repository.EmployeeAvgGpa(tahun);
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    Message = results
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("totalbymajor")]
        public async Task<ActionResult> GetTotalEmployeeByMajor()
        {
            try
            {
                var results = await _repository.TotalByMajor();
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    Message = results
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("workperiod")]
        public async Task<ActionResult> GetEmployeesWorkPeriod()
        {
            try
            {
                var results = await _repository.WorkPeriod();
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    Message = results
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
