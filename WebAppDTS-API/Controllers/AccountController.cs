using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebAppDTS_API.Base;
using WebAppDTS_API.Handlers;
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
        private readonly ITokenService _tokenService;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountController(
         IAccountRepository repository,
         ITokenService tokenService,
         IAccountRoleRepository accountRoleRepository,
         IEmployeeRepository employeeRepository) : base(repository)
        {
            _tokenService = tokenService;
            _accountRoleRepository = accountRoleRepository;
            _employeeRepository = employeeRepository;
            _repository = repository;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterVM registerVM)
        {
            try
            {
                var result = await _repository.RegisterAsync(registerVM);
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    Message = "Register berhasil!"
                });
            }
            catch
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
                //return BadRequest();
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginVM loginVM)
        {
            try
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

                var userdata = await _employeeRepository.GetUserDataByEmailAsync(loginVM.Email);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, userdata.Email),
                    new Claim(ClaimTypes.Name, userdata.Email),
                    new Claim(ClaimTypes.NameIdentifier, userdata.FullName)
                };

                var getRoles = await _accountRoleRepository.GetRolesByNikAsync(userdata.Nik);
                foreach (var item in getRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var accessToken = _tokenService.GenerateAccessToken(claims);
                //var refreshToken = _tokenService.GenerateRefreshToken();

                return Ok(accessToken);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Internal Server Error",
                    Errors = new
                    {
                        Message = "Login tidak berhasil!"
                    },
                });
            }            
        }
    }
}
