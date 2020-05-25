using FluentValidation;
using MediatR;
using Persistence.SystemCourse.Instructores;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Instructores
{
    public class New
    {
        public class Eject : IRequest
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Grade { get; set; }

            public class EjectValidate : AbstractValidator<Eject>
            {
                public EjectValidate()
                {
                    RuleFor(x => x.Name).NotEmpty();
                    RuleFor(x => x.LastName).NotEmpty();
                    RuleFor(x => x.Grade).NotEmpty();
                }

            }

            public class Handler : IRequestHandler<Eject>
            {
                private readonly IInstructor _instructorRepository;
                public Handler(IInstructor instructorRepository)
                {
                    _instructorRepository = instructorRepository;
                }
                public async Task<Unit> Handle(Eject request, CancellationToken cancellationToken)
                {
                    var result=  await _instructorRepository.New(request.Name, request.LastName, request.Grade);

                    if (result >0 )
                    {
                        return Unit.Value;
                    }

                    throw new Exception("Not Insert Data Instructor");

                }
            }
        }
    }
}
