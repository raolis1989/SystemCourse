using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.SystemCourse.HandlerError;
using AutoMapper;
using Domain.SystemCourse.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Courses
{
    public class QueryId
    {
        public class CourseUnique : IRequest<CourseDto>{
            public Guid Id{get;set;}
        }

        public class Handler : IRequestHandler<CourseUnique, CourseDto>
        {
            private readonly CoursesOnLineContext _context;
            private readonly IMapper _mapper;

            public Handler(CoursesOnLineContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CourseDto> Handle(CourseUnique request, CancellationToken cancellationToken)
            {

                var course= await _context.Course
                                    .Include(x=>x.CommentList)
                                    .Include(x=>x.PricePromotion)
                                    .Include(x=>x.InstructorsLink).ThenInclude(y=>y.Instructor)
                                    .FirstOrDefaultAsync(async=>async.CourseId  == request.Id);
                if (course==null){
                     throw new HandlerException(HttpStatusCode.NotFound, new {mensaje ="No se encontro el curso"});
                }
                var courseDto = _mapper.Map<Course, CourseDto>(course);
                return courseDto;
            }
        }
    }
}