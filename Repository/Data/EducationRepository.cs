using Web_API.Contexts;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Repository.Data
{
    public class EducationRepository : GeneralRepository<Education, int, SQLServerContext>, IEducationRepository
    {
        public EducationRepository(SQLServerContext context) : base(context) { }
    }
}
