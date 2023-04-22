using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Base;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user")]
    public class UniversityController : BaseController<IUniversityRepository, University, int>
    {
        public UniversityController(IUniversityRepository repository) : base(repository)
        {
        }
    }
}
