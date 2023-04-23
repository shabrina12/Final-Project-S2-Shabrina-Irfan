using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Net;
using WebAppDTS_API.Base;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : BaseController<IEducationRepository, Education, int>
    {
        private readonly IEducationRepository _repository;
        public EducationController(IEducationRepository repository) : base(repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public override async Task<IActionResult> InsertAsync(Education education)
        {
            var result = await _repository.InsertAsync(education);
            return Ok(result);
        }        

    }
}
