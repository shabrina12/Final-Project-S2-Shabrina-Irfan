using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API.Base;
using Web_API.Models;
using Web_API.Repository.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class UniversitiesController : GeneralController<IUniversityRepository, University, int>
    {
        public UniversitiesController(IUniversityRepository repository) : base(repository) { }
    }
}
