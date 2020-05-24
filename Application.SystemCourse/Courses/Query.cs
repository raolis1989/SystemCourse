using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.SystemCourse.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Courses
{
    public class Query
    {
        public class ListCourses : IRequest<List<CourseDto>>{}

        public class Handler : IRequestHandler<ListCourses, List<CourseDto>>
        {
            private readonly CoursesOnLineContext _context;
            private readonly IMapper _mapper;

            public Handler(CoursesOnLineContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<CourseDto>> Handle(ListCourses request, CancellationToken cancellationToken)
            {
                var course= await _context.Course.Include(x=>x.InstructorsLink)
                                    .ThenInclude(x => x.Instructor).ToListAsync();

                var courseDto = _mapper.Map<List<Course>, List<CourseDto>>(course);

                return courseDto;
            }
        }
    }
}