using Domain.SystemCourse.Entities;
using FluentValidation;
using MediatR;
using Persistence.SystemCourse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Comments
{
    public class New
    {
        public class Eject: IRequest
        {
            public string Student { get; set; }
            public int Score { get; set; }
            public string CommentText { get; set; }
            public Guid CourseId { get; set; }
        }

        public class EjectValidation : AbstractValidator<Eject>
        {
            public EjectValidation()
            {
                RuleFor(x => x.Student).NotEmpty();
                RuleFor(x => x.Score).NotEmpty();
                RuleFor(x => x.CommentText).NotEmpty();
                RuleFor(x => x.CourseId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Eject>
        {
            private readonly CoursesOnLineContext _context;
            public Handler(CoursesOnLineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle (Eject request, CancellationToken cancellationToken)
            {
                var comment = new Comment
                {
                    CommentId = Guid.NewGuid(),
                    Student = request.Student,
                    CommentText = request.CommentText,
                    CourseId = request.CourseId,
                    DateCration= DateTime.UtcNow
                };


                _context.Comment.Add(comment);
                try
                {
                    var result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        return Unit.Value;
                    }
                }
                catch (Exception es)
                {

                    throw es;
                }
             

                throw new Exception("Not Insert Data Comment");

            }
        }
    }
}
