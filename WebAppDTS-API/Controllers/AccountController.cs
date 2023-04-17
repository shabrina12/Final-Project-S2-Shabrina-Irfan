using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // GET: AccountController
        [HttpGet]
        [Route("api/account")]
        public async Task<IActionResult> Index()
        {
            var entities = await _accountRepository.GetAll();
            return View(entities);
        }
    }
}
