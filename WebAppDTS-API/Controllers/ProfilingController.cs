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
        //private readonly IProfilingRepository _repository;
        public ProfilingController(IProfilingRepository repository) : base(repository)
        {
            //_repository = repository;
        }
    }
}
