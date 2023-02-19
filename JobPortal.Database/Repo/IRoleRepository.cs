using JobPortal.Database.Infra;
using JobPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> Add(Role role);
        Task<IEnumerable<Role>> GetRoles(PagedParameters pagedParameters);
        Task<Role> GetRoleById(int id);
        Task<Role> UpdateRole(Role role);
        Task<bool> Delete(int id);
        //Task<Role> GetRoleById1(int userId);
    }
}
