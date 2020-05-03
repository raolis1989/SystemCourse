using System.Collections.Generic;

namespace Domain.SystemCourse.Entities
{
    public class  Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public byte[] PhotoPerfil { get; set; }

        public ICollection<CourseInstructor> CoursesLink { get; set; }
    }
}