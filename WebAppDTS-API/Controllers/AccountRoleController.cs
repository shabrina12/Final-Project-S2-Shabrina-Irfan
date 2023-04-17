using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : Controller
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _accountRoleRepository.GetAllAsync();
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
    }
}
