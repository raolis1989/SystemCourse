using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.SystemCourse.Comments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.SystemCourse.Controllers
{
    public class CommentController : GenericControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Eject data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid Id)
        {
            return await Mediator.Send(new Delete.Eject { Id = Id });
        }

    }
}