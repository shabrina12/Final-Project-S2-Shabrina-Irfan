using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebAppDTS_API.Base;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository;
using WebAppDTS_API.Repository.Contracts;
using WebAppDTS_API.ViewModels;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<IAccountRepository, Account, string>
    {
        private readonly IAccountRepository _repository;

        public AccountController(IAccountRepository repository) : base(repository)
        {
           _repository = repository;        
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterVM registerVM)
        {
            var result = await _repository.RegisterAsync(registerVM);

            if (result == 0)
            {
                return Conflict(new
                {
                    code = StatusCodes.Status409Conflict,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Register tidak berhasil!"
                    }
                });
            }
            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                Message = "Register berhasil!"
            });
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginVM loginVM)
        {
            var result = await _repository.LoginAsync(loginVM);
            if (!result)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Email atau Password tidak ditemukan!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Login berhasil!"
                }
            });
        }
    }
}
