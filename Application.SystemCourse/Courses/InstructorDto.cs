using System;

namespace Application.SystemCourse.Courses
{
    public class InstructorDto
    {
       public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public byte[] PhotoPerfil { get; set; }

      
    }
}