using Application.SystemCourse.HandlerError;
using MediatR;
using Persistence.SystemCourse;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Comments
{
    public class Delete
    {
        public class Eject : IRequest
        {
            public Guid Id { get; set; }
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
                var comment = await _context.Comment.FindAsync(request.Id);
                if (comment == null)
                {
                    throw new HandlerException(HttpStatusCode.NotFound, new { messaje = "Not Found Data" });
                }

                _context.Remove(comment);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Not Data Delete");
            }
        }
    }
}
