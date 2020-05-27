using Application.SystemCourse.HandlerError;
using Domain.SystemCourse.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Security
{
    public class ObtainRolesForUser
    {
        public class Eject : IRequest<List<string>>
        {
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Eject, List<string>>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<List<string>>Handle (Eject request, CancellationToken cancellationToken)
            {
                var userIden = await _userManager.FindByNameAsync(request.UserName);
                if (userIden == null)
                {
                    throw new HandlerException(HttpStatusCode.NotFound, new { messaje = "Not Exist User" });

                }

                var result = await _userManager.GetRolesAsync(userIden);
                return new List<string>(result); 

            }


         
        }
    }
}
