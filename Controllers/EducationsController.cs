using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API.Base;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EducationsController : GeneralController<IEducationRepository, Education, int>
    {
        public EducationsController(IEducationRepository repository) : base(repository) {}

        // POST - ADD
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> PostAsync(Education entity)
        {
            return await base.PostAsync(entity);
        }

        // PUT: api/<TEntity>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> PutAsync(int id, Education entity)
        {
            return await base.PutAsync(id, entity);
        }
        // DELETE: api/<TEntity>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> DeleteAsync(int id)
        {
            return await base.DeleteAsync(id);
        }
    }
}
