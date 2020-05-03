namespace Domain.SystemCourse.Entities
{
    public class Price
    {
        public int PriceId { get; set; }
        public decimal PriceActual { get; set; }
        public decimal Promotion { get; set; }
        public int CourseId { get; set; }
        public  Course Course { get; set; }
    }
}