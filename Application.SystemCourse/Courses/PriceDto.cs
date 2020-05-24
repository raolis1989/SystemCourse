using System;

namespace Application.SystemCourse.Courses
{
    public class PriceDto
    {
        public Guid PriceId { get; set; }
        public decimal PriceActual { get; set; }
        public decimal Promotion { get; set; }
        public Guid CourseId { get; set; }
    }
}