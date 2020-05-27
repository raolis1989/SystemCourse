using Application.SystemCourse.HandlerError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Security
{
    public class DeleteRol
    {
        public class  Eject : IRequest
        {
            public string Name { get; set; }
        }
        public class ValidateEject : AbstractValidator<Eject>
        {
            public ValidateEject()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Eject>
        {
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Eject request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.Name);

                if (role == null)
                {
                    throw new HandlerException(HttpStatusCode.BadRequest, new { messaje = "Rol Not Exist!" });

                }

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                throw new Exception("Rol Not Deleted");
            }
        }
    }
}
