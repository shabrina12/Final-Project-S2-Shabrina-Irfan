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
        public EducationController(IEducationRepository repository) : base(repository) {}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> InsertAsync(Education education)
        {
            return await base.InsertAsync(education);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> UpdateAsync(Education education, int id)
        {
            return await base.UpdateAsync(education, id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
