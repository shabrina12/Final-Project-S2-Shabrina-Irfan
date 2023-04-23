using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppDTS_API.Repository;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<IRepository, Entity, Key>: Controller
        where Entity : class
        where IRepository : IGeneralRepository<Entity, Key>
    {
        private readonly IRepository _repository;
        public BaseController(IRepository repository)
        {
            _repository = repository;
        }

        // GET ALL 
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET BY ID
        [HttpGet("{key}")]
        public async Task<IActionResult> GetByIdAsync(Key key)
        {
            var result = await _repository.GetByIdAsync(key);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // INSERT
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public virtual async Task<IActionResult> InsertAsync(Entity entity)
        {
            var result = await _repository.InsertAsync(entity);
            return Ok(result);
        }

        // UPDATE
        [HttpPut("{key}")]
        public async Task<IActionResult> UpdateAsync(Entity entity, Key key)
        {
            if (key.Equals(entity.GetType().GetProperty("Id")) || key.Equals(entity.GetType().GetProperty("Nik")))
            {
                return BadRequest();
            }

            if (!await _repository.IsExist(key))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(entity);
            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Data berhasil diupdate!"
                }
            });
        }

        // DELETE
        [HttpDelete("{key}")]
        public virtual async Task<IActionResult> Delete(Key key)
        {
            var result = await _repository.DeleteAsync(key);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Data berhasil dihapus!"
                }
            });
        }

    }
}
