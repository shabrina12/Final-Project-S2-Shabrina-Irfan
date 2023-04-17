using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Base;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<IAccountRepository, Account, string>
    {
        //private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository) : base(repository)
        {
           // this.repository = repository;        
        }

    }
}
