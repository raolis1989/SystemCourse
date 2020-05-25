using System;

namespace Application.SystemCourse.Courses
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Score { get; set; }
        public string CommentText { get; set; }
        public Guid CourseId { get; set; }
        public DateTime? DateCreation { get; set; }

    }
}