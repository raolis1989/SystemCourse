using System.Collections.Generic;
using System.Threading.Tasks;
using Application.SystemCourse.Interfaces;
using Domain.SystemCourse.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.SystemCourse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<List<Course>> Get()
        {
           return  await _courseRepository.ObtainCourses();
        }
    }
}