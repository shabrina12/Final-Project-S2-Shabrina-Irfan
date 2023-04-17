using Microsoft.EntityFrameworkCore;
using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;

namespace WebAppDTS_API.Repository
{
    public class UniversityRepository : GeneralRepository<University, int, MyContext>, IUniversityRepository
    {
        public UniversityRepository(MyContext context) : base(context) { }
        public async Task<bool> IsNameExist(string name)
        {
            var entity = await _context.Set<University>().FirstOrDefaultAsync(x => x.Name == name);
            return entity != null;
        }
    }
}
