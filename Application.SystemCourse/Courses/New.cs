using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Domain.SystemCourse.Entities;
using FluentValidation;
using MediatR;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Courses
{
    public class New
    {
        public class Eject : IRequest{
           
            public string Title {get;set;}
            public string Description { get; set; }
            public DateTime? DatePublish { get; set; }

            public List<Guid> ListInstructor { get; set; }
            public decimal Price {get;set;}
            public decimal Promotion {get;set;}
        }

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

            public async Task<Unit> Handle(Eject request, CancellationToken cancellationToken)
            {
                Guid _courseID = Guid.NewGuid();
                var course = new  Course{
                     CourseId= _courseID,
                     Title= request.Title,
                     Description = request.Description,
                     DatePublish = request.DatePublish,
                     DateCreation = DateTime.UtcNow
                };
                _context.Course.Add(course);

                if(request.ListInstructor!=null)
                {
                    foreach(var id in request.ListInstructor)
                    {
                       var courseInstructor = new CourseInstructor
                        {
                            CourseId= _courseID, 
                            InstructorId= id
                        };
                        _context.CourseInstructor.Add(courseInstructor);
                    }
                }

                var priceEntitie = new Price
                {
                    CourseId =_courseID,
                    PriceActual= request.Price,
                    Promotion = request.Promotion,
                    PriceId= Guid.NewGuid()
                };
                
                _context.Price.Add(priceEntitie);

                var value= await _context.SaveChangesAsync();
                if(value>0){
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}