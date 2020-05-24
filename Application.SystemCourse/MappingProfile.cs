using System.Linq;
using Application.SystemCourse.Courses;
using AutoMapper;
using Domain.SystemCourse.Entities;

namespace Application.SystemCourse
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course,CourseDto>()
            .ForMember(x=>x.Instructors, y =>y.MapFrom(z=>z.InstructorsLink.Select(a =>a.Instructor).ToList()));
            CreateMap<CourseInstructor, CourseInstructorDto>();
            CreateMap<Instructor, InstructorDto>();
        }
    }
}