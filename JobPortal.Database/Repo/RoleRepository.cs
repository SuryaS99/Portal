using JobPortal.Database.Infra;
using JobPortal.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly JobDbContext _jobDbContext;

        public RoleRepository(JobDbContext jobDbContext) :base(jobDbContext)
        {
            _jobDbContext = jobDbContext;
        }

        public async Task<Role> Add(Role role)
        {
            var _role = await _jobDbContext.Role.AddAsync(role);
            await _jobDbContext.SaveChangesAsync();
            return role;
        }

        public async Task<bool> Delete(int id)
        {
           // try
           // {
                Role _role = await _jobDbContext.Role.Where(x => x.Id == id).FirstOrDefaultAsync();
               // if (_role != null)
               // {
                    _jobDbContext.Remove(_role);
                    await _jobDbContext.SaveChangesAsync();
                    return true;
               // }                
               // return false;
          //  }
           // catch (Exception ex)
           // {

               // Log.Error("Error occurred : {0}", ex);
           // }
           // return false;
        }

        public async Task<IEnumerable<Role>> GetRoles(PagedParameters pagedParameters)
        {
            var roles = await(from r in JobDbContext.Role
                              select r)
                        .Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                        .Take(pagedParameters.PageSize)
                        .ToListAsync();
            return roles;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await _jobDbContext.Role.Where(x => x.Id == id).FirstOrDefaultAsync();
            return role;

        }

        public async Task<Role> UpdateRole(Role role)
        {
            var _role = await _jobDbContext.Role.FirstOrDefaultAsync(r=>r.Id==role.Id);
            if (_role != null) 
            {
                _role.Roles = role.Roles;
            }
            _jobDbContext.Entry(_role).State = EntityState.Modified;
            await _jobDbContext.SaveChangesAsync();
            return _role;
           
        }
        public async Task<Role> GetRoleById1(int userid)
        {
            var role = await _jobDbContext.Role.Where(x => x.Id == userid).FirstOrDefaultAsync();
            return role ;
        }
    }
}
