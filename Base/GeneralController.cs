using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API.Repository.Contracts;

namespace Web_API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeneralController<TIRepository, TEntity, TKey> : ControllerBase
    where TEntity : class
    where TIRepository : IGeneralRepository<TEntity, TKey>
    {
        protected TIRepository _repository;

        public GeneralController(TIRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<TEntity>
        [HttpGet]
        public virtual async Task<ActionResult> GetAsync()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/<TEntity>/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetAsync(TKey id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/<TEntity>
        [HttpPost]
        public virtual async Task<ActionResult> PostAsync(TEntity entity)
        {
            var result = await _repository.InsertAsync(entity);
            return Ok(result);
        }

        // PUT: api/<TEntity>/5
        [HttpPut("{id}")]
        public virtual async Task<ActionResult> PutAsync(TKey id, TEntity entity)
        {
            if (id.Equals(entity.GetType().GetProperty("Id")) ||
                id.Equals(entity.GetType().GetProperty("Nik")))
            {
                return BadRequest();
            }

            if (!await _repository.IsExist(id))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }
        // DELETE: api/<TEntity>/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> DeleteAsync(TKey id)
        {
            if (!await _repository.IsExist(id))
            {
                return NotFound();
            }

            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity!);
            return NoContent();
        }
    }
}
