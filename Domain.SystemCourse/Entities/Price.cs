using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.SystemCourse.Entities
{
    public class Price
    {
        public Guid PriceId { get; set; }
        [Column(TypeName="decimal(18,4")]
        public decimal PriceActual { get; set; }
        [Column(TypeName="decimal(18,4")]
        public decimal Promotion { get; set; }
        public Guid CourseId { get; set; }
        public  Course Course { get; set; }
    }
}