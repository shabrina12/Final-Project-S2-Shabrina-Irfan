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
    public class EmployeeController : BaseController<IEmployeeRepository, Employee, string>
    {
        public EmployeeController(IEmployeeRepository repository) : base(repository)
        {
        }
    }
}
