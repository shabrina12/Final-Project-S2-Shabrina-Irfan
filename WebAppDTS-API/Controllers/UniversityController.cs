using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _universityRepository.GetAllAsync();
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

        //[HttpGet]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var results = await _universityRepository.GetByIdAsync(id);
        //    if (results == null)
        //    {
        //        return NotFound(new
        //        {
        //            code = StatusCodes.Status404NotFound,
        //            status = HttpStatusCode.NotFound.ToString(),
        //            data = new
        //            {
        //                message = "Data tidak ditemukan!"
        //            }
        //        });
        //    }

        //    return Ok(new
        //    {
        //        code = StatusCodes.Status200OK,
        //        status = HttpStatusCode.OK.ToString(),
        //        data = results
        //    });
        //}

        //public async Task<IActionResult> InsertAsync(University university)
        //{

        //}
    }
}
