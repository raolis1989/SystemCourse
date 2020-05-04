using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.SystemCourse.HandlerError;
using Domain.SystemCourse.Entities;
using MediatR;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Courses
{
    public class QueryId
    {
        public class CourseUnique : IRequest<Course>{
            public int Id{get;set;}
        }

        public class Handler : IRequestHandler<CourseUnique, Course>
        {
            private readonly CoursesOnLineContext _context;

            public Handler(CoursesOnLineContext context)
            {
                _context = context;
            }

            public async Task<Course> Handle(CourseUnique request, CancellationToken cancellationToken)
            {

                var course= await _context.Course.FindAsync(request.Id);
                if (course==null){
                     throw new HandlerException(HttpStatusCode.NotFound, new {mensaje ="No se encontro el curso"});
                }
                return course;
            }
        }
    }
}