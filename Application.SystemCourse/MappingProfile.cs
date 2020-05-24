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
            .ForMember(x=>x.Instructors, y =>y.MapFrom(z=>z.InstructorsLink.Select(a =>a.Instructor).ToList()))
            .ForMember(x=>x.Comments, y=>y.MapFrom(z=>z.CommentList))
            .ForMember(x=>x.Price, y=>y.MapFrom(z=>z.PricePromotion));
            CreateMap<CourseInstructor, CourseInstructorDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Price, PriceDto>();
        }
    }
}