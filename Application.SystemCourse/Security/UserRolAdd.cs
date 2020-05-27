using Application.SystemCourse.HandlerError;
using Domain.SystemCourse.Entities;
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
    public class UserRolAdd
    {
        public class Eject : IRequest
        {
            public string UserName{ get; set; }
            public string RolName { get; set; }
        }

        public class ValidateEject : AbstractValidator<Eject>
        {
            public ValidateEject()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RolName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Eject>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Identityrole> _roleManager;

            public Handler (UserManager<User> userManager, RoleManager<Identityrole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Eject request, CancellationToken canccelationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolName);
                if (role == null)
                {
                    throw new HandlerException(HttpStatusCode.NotFound, new { messaje = "Rol Not Exist" });
                }

                var userToken = await _userManager.FindByNameAsync(request.RolName);
                if(userToken==null)
                {
                    throw new HandlerException(HttpStatusCode.NotFound, new { messaje = "User Not Exist" });
                }

                var result = await _userManager.AddToRoleAsync(userToken, request.RolName);
                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                throw new Exception("rol not added to user"); 
            }
        }
    }
}
