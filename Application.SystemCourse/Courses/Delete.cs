using System;
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
            public int Id{get;set;}

            public class Handler : IRequestHandler<Eject>
            {
                private readonly CoursesOnLineContext _context;

                public Handler(CoursesOnLineContext context)
                {
                    _context = context;
                }

                public async Task<Unit> Handle(Eject request, CancellationToken cancellationToken)
                {
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