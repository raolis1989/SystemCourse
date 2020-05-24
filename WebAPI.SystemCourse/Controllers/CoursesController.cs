using System.Collections.Generic;
using System.Threading.Tasks;
using Application.SystemCourse.Courses;
using Domain.SystemCourse.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.SystemCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : GenericControllerBase
    {

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CourseDto>>> Get()
        {
           return await  Mediator.Send(new Query.ListCourses());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Detail(int id)
        {
            return await Mediator.Send(new QueryId.CourseUnique{Id=id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Eject data){
            
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Eject data){
            data.CourseId=id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>>Delete(int id){
            return await Mediator.Send(new Delete.Eject{Id=id});
        }


    }
}