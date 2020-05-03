using Domain.SystemCourse.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.SystemCourse
{
    public class CoursesOnLineContext : DbContext
    {
        public CoursesOnLineContext(DbContextOptions options): base(options)
        {
            
        }

         protected override void OnModelCreating(ModelBuilder modelBuilder){
             modelBuilder.Entity<CourseInstructor>().HasKey(ci => new { ci.InstructorId, ci.CourseId});
         }

         public DbSet<Comment> Comments { get; set; }
         public DbSet<Course> Courses { get; set; }
         public DbSet<CourseInstructor> CourseInstructors {get;set;}
         public DbSet<Instructor> Instructors {get;set;}
         public DbSet<Price> Prices {get;set;}
    }
}