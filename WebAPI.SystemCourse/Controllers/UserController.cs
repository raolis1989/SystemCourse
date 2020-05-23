using System.Threading.Tasks;
using Application.SystemCourse.Security;
using Domain.SystemCourse.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.SystemCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : GenericControllerBase
    {
         [HttpPost("login")]
         public async Task<ActionResult<UserData>> Login(Login.Eject parameters)
         {
             return await Mediator.Send(parameters);
         }

    }
}