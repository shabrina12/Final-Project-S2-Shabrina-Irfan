using Microsoft.AspNetCore.Mvc;
using Web_API.Base;
using Web_API.DataModels;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : GeneralController<IEmployeeRepository, Employee, string>
    {
        public EmployeesController(IEmployeeRepository repository) : base(repository) { }

        [HttpGet("master")]
        public async Task<ActionResult> GetProfile()
        {
            var result = await _repository.GetAllProfileAsync();
            ResultFormat resultFormat = new ResultFormat
            {
                Data = result,
            };
            return Ok(resultFormat);
        }
    }
}
