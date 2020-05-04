using System;
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
                var course = new  Course{
                     Title= request.Title,
                     Description = request.Description,
                     DatePublish = request.DatePublish
                };
                _context.Course.Add(course);
                var value= await _context.SaveChangesAsync();
                if(value>0){
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}