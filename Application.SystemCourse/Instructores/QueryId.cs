using Application.SystemCourse.HandlerError;
using MediatR;
using Persistence.SystemCourse.Instructores;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Instructores
{
    public class QueryId
    {
        public class Eject : IRequest<InstructorModel>
        {
            public Guid Id { get; set; }
        }

        public class Handler: IRequestHandler<Eject, InstructorModel>
        {
            public readonly IInstructor _instructorRepository; 
            public Handler(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;
            }

            public async Task<InstructorModel> Handle (Eject request, CancellationToken cancellationToken)
            {
                var instructor = await _instructorRepository.ObtainForId(request.Id);
                if (instructor == null)
                {
                    throw new HandlerException(HttpStatusCode.NotFound, new { messaje = "Not Found Instructor" }); 
                }
                return instructor;
            }
        }
    }
}
