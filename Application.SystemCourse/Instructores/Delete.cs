using Domain.SystemCourse.Entities;
using MediatR;
using Persistence.SystemCourse.Instructores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Instructores
{
    public class Delete
    {
        public class Eject : IRequest {
            public Guid Id { get; set; }
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
                var resultados = await _instructorRepository.Delete(request.Id);

                if (resultados > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Not Data Instructor Delete");
            }
        }
    }
}
