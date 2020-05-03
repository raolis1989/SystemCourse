using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.SystemCourse.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Courses
{
    public class Query
    {
        public class ListCourses : IRequest<List<Course>>{}

        public class Handler : IRequestHandler<ListCourses, List<Course>>
        {
            private readonly CoursesOnLineContext _context;

            public Handler(CoursesOnLineContext context)
            {
                _context = context;
            }
            public async Task<List<Course>> Handle(ListCourses request, CancellationToken cancellationToken)
            {
                return await _context.Course.ToListAsync();
            }
        }
    }
}