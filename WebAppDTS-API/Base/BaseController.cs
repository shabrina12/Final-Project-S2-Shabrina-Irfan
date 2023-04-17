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
        private readonly IRepository repository;
        public BaseController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET ALL 
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var results = await repository.GetAllAsync();
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

        // GET BY ID
        [HttpGet("{key}")]
        public async Task<IActionResult> GetByIdAsync(Key key)
        {
            var result = await repository.GetByIdAsync(key);
            if (result == null)
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
                data = result
            });
        }

        // INSERT
        [HttpPost]
        public async Task<IActionResult> InsertAsync(Entity entity)
        {
            var result = await repository.InsertAsync(entity);
            if (result == null)
            {
                return Conflict(new
                {
                    code = StatusCodes.Status409Conflict,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data tidak berhasil disimpan!"
                    }
                });
            }
            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Data berhasil disimpan!"
                }
            });
        }

        // UPDATE
        [HttpPut("{key}")]
        public async Task<IActionResult> UpdateAsync(Entity entity, Key key)
        {
            var result = await repository.IsExist(key);
            if (!result)
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
            var update = await repository.UpdateAsync(entity);
            if (update == 0)
            {
                return Conflict(new
                {
                    code = StatusCodes.Status409Conflict,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data tidak berhasil diupdate!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Data berhasil terupdate!"
                }
            });
        }

        // DELETE
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(Key key)
        {
            var result = await repository.DeleteAsync(key);
            if (result == 0)
            {
                return Conflict(new
                {
                    code = StatusCodes.Status409Conflict,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data tidak berhasil dihapus!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Data berhasil terhapus!"
                }
            });
        }

    }
}
