using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.SystemCourse.HandlerError;
using FluentValidation;
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

            public class EjectValidation : AbstractValidator<Eject>{
            public EjectValidation(){
                RuleFor(x=>x.Title).NotEmpty();
                RuleFor(x=>x.Description).NotEmpty();
                RuleFor(x=>x.DatePublish).NotEmpty();

            }
        }


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
                         throw new HandlerException(HttpStatusCode.NotFound, new {mensaje ="No se encontro el curso"});
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