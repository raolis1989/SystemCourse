using System;
using System.Collections;
using System.Collections.Generic;

namespace Application.SystemCourse.Courses
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DatePublish { get; set; }
        public Byte[] Picture { get; set; }
        public ICollection<InstructorDto> Instructors { get; set; }
        public PriceDto Price {get;set;}
        public DateTime? DateCreation { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        
        
    }
}