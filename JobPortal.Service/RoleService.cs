using JobPortal.Database.Repo;
using JobPortal.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> Add(Role role)
        {
            var _role = await _roleRepository.AddAsync(role);
            return _role;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Role role = await _roleRepository.GetById(id);
                if (role != null)
                {
                    await _roleRepository.Delete(id);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                Log.Error("Error occurred : {0}", ex);
            }
           
            return false;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await _roleRepository.GetRoleById(id);
            return role;
        }

        public async Task<IEnumerable<Role>> GetRoles(PagedParameters pagedParameters)
        {
           return await _roleRepository.GetRoles(pagedParameters);
        }

        public async Task<Role> UpdateRole(Role role)
        {
            var _role = await _roleRepository.UpdateRole(role);
            return _role;
        }
        public async Task<Role> GetRoleById1(int userid)
        {
            return await _roleRepository.GetById(userid);
        }
    }
}
