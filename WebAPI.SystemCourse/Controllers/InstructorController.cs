using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.SystemCourse.Instructores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.SystemCourse.Instructores;

namespace WebAPI.SystemCourse.Controllers
{
    public class InstructorController : GenericControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> Obtaininstructors()
        {
            return await Mediator.Send(new Query.ListIstructor());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Eject data)
        {
            return await Mediator.Send(data);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(Guid id, Edit.Eject data)
        {
            data.InstructorId = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Eject { Id = id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorModel>>ObtainForId(Guid id)
        {
            return await Mediator.Send(new QueryId.Eject { Id = id });
        }

    }
}