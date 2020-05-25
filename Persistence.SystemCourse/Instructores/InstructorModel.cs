using System;

namespace Persistence.SystemCourse.Instructores
{
    public class InstructorModel
    {
       public Guid InstructorId { get; set; }  
       public string Name { get; set; }
       public string LastName { get; set; }
       public string Grade { get; set; }
        public DateTime? DateCreation { get; set; }
    }
}