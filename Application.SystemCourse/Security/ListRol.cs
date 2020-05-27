using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.SystemCourse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Security
{
    public class ListRol
    {
        public class Eject : IRequest<List<IdentityRole>>
        {
                
          
        }

        public class Handler : IRequestHandler<Eject, List<IdentityRole>>
        {
            private readonly CoursesOnLineContext _context;
            public Handler(CoursesOnLineContext context)
            {
                _context = context;
            }

            public async Task<List<IdentityRole>> Handle(Eject request, CancellationToken cancellationToken)
            {
                var roles =await _context.Roles.ToListAsync();
                return roles; 
            }

        }
    }
}
