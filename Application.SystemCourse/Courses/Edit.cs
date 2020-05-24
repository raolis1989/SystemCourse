using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.SystemCourse.HandlerError;
using Domain.SystemCourse.Entities;
using FluentValidation;
using MediatR;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Courses
{
    public class Edit
    {
        public class Eject : IRequest {
            public Guid CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? DatePublish { get; set; }
            public List<Guid> ListInstructor { get; set; }
            public decimal? Price {get;set;}
            public decimal? Promotion{get;set;}

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

                     var priceEntitie = _context.Price.Where(x=>x.CourseId== course.CourseId).FirstOrDefault();

                     if (priceEntitie!=null)
                     {
                         priceEntitie.Promotion= request.Promotion ?? priceEntitie.Promotion;
                         priceEntitie.PriceActual= request.Price ?? priceEntitie.PriceActual;
                     }
                     else
                     {
                         priceEntitie = new Price 
                         {
                             PriceId = Guid.NewGuid(),
                             PriceActual= request.Price ?? 0,
                             Promotion= request.Promotion?? 0,
                             CourseId = course.CourseId
                         };
                         await _context.Price.AddAsync(priceEntitie);
                     }

                     if(request.ListInstructor!=null)
                     {
                         if(request.ListInstructor.Count>0)
                         {
                             var instructorsBD = _context.CourseInstructor.Where(x=>x.CourseId == request.CourseId).ToList();
                             foreach(var instructoEliminar in instructorsBD)
                             {
                                 _context.CourseInstructor.Remove(instructoEliminar);
                             }

                             foreach(var ids in request.ListInstructor)
                             {
                                 var newInstructor = new CourseInstructor
                                 {
                                     CourseId= request.CourseId,
                                     InstructorId= ids
                                 };

                                 _context.CourseInstructor.Add(newInstructor);
                             }
                         }
                     }
               
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