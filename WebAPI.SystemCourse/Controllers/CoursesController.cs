using System.Collections.Generic;
using System.Threading.Tasks;
using Application.SystemCourse.Courses;
using Domain.SystemCourse.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.SystemCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Get()
        {
           return await  _mediator.Send(new Query.ListCourses());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Detail(int id)
        {
            return await _mediator.Send(new QueryId.CourseUnique{Id=id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Eject data){
            
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Eject data){
            data.CourseId=id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>>Delete(int id){
            return await _mediator.Send(new Delete.Eject{Id=id});
        }


    }
}