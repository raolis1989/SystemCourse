using System.Threading.Tasks;
using Application.SystemCourse.Security;
using Domain.SystemCourse.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.SystemCourse.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UserController : GenericControllerBase
    {
         [HttpPost("login")]
         public async Task<ActionResult<UserData>> Login(Login.Eject parameters)
         {
             return await Mediator.Send(parameters);
         }
         
         [HttpPost("register")]
         public async Task<ActionResult<UserData>> Register(Register.Eject parameters)
         {
             return await Mediator.Send(parameters);
         }

         [HttpGet]
         public async Task<ActionResult<UserData>> ObtainUser(){
             
             return await Mediator.Send(new UserActual.Eject());
         }

    }
}