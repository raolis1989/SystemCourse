using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.SystemCourse.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.SystemCourse.Controllers
{
    public class RolController : GenericControllerBase
    {
       [HttpPost("Create")]
       public async Task<ActionResult<Unit>>Create(NewRol.Eject paramaters)
        {
            return await Mediator.Send(paramaters);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Unit>> Delete(DeleteRol.Eject paramaters)
        {
            return await Mediator.Send(paramaters);
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<IdentityRole>>> List()
        {
            return await Mediator.Send(new ListRol.Eject());
        }

    }
}