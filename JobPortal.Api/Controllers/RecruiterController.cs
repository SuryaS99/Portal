using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPortal.Service.Permission;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        private IServiceProvider _serviceProvider;
        public RecruiterController(
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        [IsAllowed("RecruiterAdd")]
        public async Task<IActionResult> Add(User user)
        {
            if (ModelState.IsValid)
            {
                IUserService helper = (IUserService)_serviceProvider.GetService(typeof(IUserService));

                var isExist = await helper.IsEmailAlreadyExist(user.Email);
                if (isExist == true)
                {
                    return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Email Already exist.." });
                }
                else
                {
                    var result = await helper.Add(user);
                    return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Account successfully created...", Data = result });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("Recruiters1")]
        public async Task<IActionResult> GetRecuiters1([FromServices] IRecruiterService _recruiterService, [FromQuery] PagedParameters pagedParameters)
        {
            return Ok(await _recruiterService.GetRecruiters(pagedParameters));
        }

    }
}
