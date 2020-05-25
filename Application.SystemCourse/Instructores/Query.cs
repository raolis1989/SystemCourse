using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence.SystemCourse.Instructores;

namespace Application.SystemCourse.Instructores
{
    public class Query
    {
        public class ListIstructor : IRequest<List<InstructorModel>> {}

        public class Handler : IRequestHandler<ListIstructor, List<InstructorModel>>
        {
            private readonly IInstructor _instructorRepository;
            public Handler (IInstructor instructorRepository)
            {
                _instructorRepository=instructorRepository;
            }
            public async Task<List<InstructorModel>> Handle(ListIstructor request, CancellationToken cancellationToken)
            {
               var res= await _instructorRepository.ObtainList();
               return res.ToList();
            }
        }
    }
}