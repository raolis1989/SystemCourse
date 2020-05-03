namespace Domain.SystemCourse.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Student { get; set; }
        public int Score { get; set; }
        public string CommentText { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}