using Web_API.Contexts;
using Web_API.Models;
using Web_API.Repository.Contracts;

namespace Web_API.Repository.Data
{
    public class RoleRepository : GeneralRepository<Role, int, SQLServerContext>, IRoleRepository
    {
        public RoleRepository(SQLServerContext context) : base(context) { }
    }
}
