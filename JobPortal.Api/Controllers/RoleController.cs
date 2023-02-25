using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> Add(Role role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.Add(role);
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Role successfully created...", Data = result });
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("Roles")]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> Get([FromQuery] PagedParameters pagedParameters)
        {
            var data = await _roleService.GetRoles(pagedParameters);
            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roleService.GetRoleById(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("role1")]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(Role role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.UpdateRole(role);
                if (result != null)
                    return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Details updated successfully..", Data = result });
                return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Something went wrong", Data = result });
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id}")]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.Delete(id);
            if (result == true)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Role deleted successfully.." });
            }
            return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Role not found.." });
        }

    }
}
