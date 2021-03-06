using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.SystemCourse.HandlerError;
using MediatR;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Courses
{
    public class Delete
    {
        public class Eject : IRequest {
            public Guid Id{get;set;}

            public class Handler : IRequestHandler<Eject>
            {
                private readonly CoursesOnLineContext _context;

                public Handler(CoursesOnLineContext context)
                {
                    _context = context;
                }

                public async Task<Unit> Handle(Eject request, CancellationToken cancellationToken)
                {
                    var instructorsDB =_context.CourseInstructor.Where(x => x.CourseId ==request.Id);

                    foreach(var instructor in instructorsDB)
                    {
                        _context.CourseInstructor.Remove(instructor);
                    }

                    var comentsDB = _context.Comment.Where(x=>x.CourseId==request.Id);
                    foreach(var cmt in comentsDB)
                    {
                        _context.Comment.Remove(cmt);
                    }

                    var priceDB = _context.Price.Where(x=>x.CourseId == request.Id).FirstOrDefault();
                    if(priceDB!=null)
                    {
                        _context.Price.Remove(priceDB);
                    }


                    var course = await _context.Course.FindAsync(request.Id);
                    if(course==null)
                    {
                        //throw new Exception("No se puede eliminar el curso");
                        throw new HandlerException(HttpStatusCode.NotFound, new {mensaje ="No se encontro el curso"});
                   }

                    _context.Remove(course);

                    var result = await _context.SaveChangesAsync();

                    if(result>0){
                        return Unit.Value;
                    }

                    //throw new Exception("No se pudieron guardar los cambios");
                    throw new HandlerException(HttpStatusCode.NotFound, new { mensaje = "No se pudieron guardar los cambios"});
                }
            }
        }
    }
}