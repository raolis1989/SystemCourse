using System;
using System.Collections.Generic;

namespace Domain.SystemCourse.Entities
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DatePublish { get; set; }
        public byte[] Picture { get; set; }
        public DateTime? DateCreation { get; set; }
        public Price PricePromotion { get; set; }
        public ICollection<Comment> CommentList   { get; set; }
        public ICollection<CourseInstructor> InstructorsLink { get; set; }
    }
}