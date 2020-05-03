using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.SystemCourse.Entities;

namespace Application.SystemCourse.Interfaces
{
    public interface ICourseRepository
    {
         Task<List<Course>> ObtainCourses();
    }
}