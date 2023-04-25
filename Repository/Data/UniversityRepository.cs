using Microsoft.EntityFrameworkCore;
using Web_API.Contexts;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<University, int, SQLServerContext>, IUniversityRepository
    {
        public UniversityRepository(SQLServerContext context) : base(context) { }

        public async Task<bool> IsNameExist(string name)
        {
            var entity = await _context.Set<University>().FirstOrDefaultAsync(x => x.Name == name);
            return entity != null;
        }

    }
}
