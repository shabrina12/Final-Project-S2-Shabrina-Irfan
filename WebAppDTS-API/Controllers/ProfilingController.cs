using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Repository;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingController : Controller
    {
        private readonly IProfilingRepository _profilingRepository;

        public ProfilingController(IProfilingRepository profilingRepository)
        {
            _profilingRepository = profilingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _profilingRepository.GetAllAsync();
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

        // GET PROFILING BY NIK
        [HttpGet("{nik}", Name = "GetProfilingByNik")]
        public async Task<IActionResult> GetById(string nik)
        {
            var result = await _profilingRepository.GetByIdAsync(nik);
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
