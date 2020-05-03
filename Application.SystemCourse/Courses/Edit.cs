using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Courses
{
    public class Edit
    {
        public class Eject : IRequest {
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? DatePublish { get; set; }

            public class Handler : IRequestHandler<Eject>
            {
                private readonly CoursesOnLineContext _context;

                public Handler(CoursesOnLineContext context)
                {
                    _context = context;
                }

                public async  Task<Unit> Handle(Eject request, CancellationToken cancellationToken)
                {
                    var course = await _context.Course.FindAsync(request.CourseId);
                     if (course==null)
                     {
                         throw new Exception("El curso no existe");
                     }

                     course.Title = request.Title ?? course.Title;
                     course.Description = request.Description ?? course.Description;
                     course.DatePublish = request.DatePublish ?? course.DatePublish;
               
                     var result = await _context.SaveChangesAsync();
                     
                     if(result>0){
                         return Unit.Value;
                     }
                     throw new Exception("No se guardaron los cambios en el curso");
                }
            }
        }
    }
}