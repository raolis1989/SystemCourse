using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.SystemCourse.Interfaces;
using Domain.SystemCourse.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.SystemCourse;


namespace Application.SystemCourse.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CoursesOnLineContext _context;

        public CourseRepository(CoursesOnLineContext context)
        {
            _context = context;
        }
        public async Task<List<Course>> ObtainCourses() => await _context.Course.ToListAsync();
    }
}