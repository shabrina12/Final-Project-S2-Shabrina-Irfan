using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Repository;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : Controller
    {
        private readonly IEducationRepository _educationRepository;
        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _educationRepository.GetAllAsync();
            if (results == null)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data Not Found!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = results
            });
        }

        // GET EDUCATION BY ID
        [HttpGet("{id}", Name = "GetEducationById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _educationRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data tidak ditemukan!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = result
            });
        }
    }
}
