using Application.SystemCourse.Contracts;
using Application.SystemCourse.HandlerError;
using Domain.SystemCourse.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using Persistence.SystemCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Security
{
    public class UserUpdate
    {
        public class Eject : IRequest<UserData>
        {
            public string Name { get; set; }
            public string LastNames { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class EjectValidator : AbstractValidator<Eject>
        {
            public EjectValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastNames).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();

            }
        }
        public class Handler : IRequestHandler<Eject, UserData>
        {
            private readonly CoursesOnLineContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IPasswordHasher<User> _passwordHasher;

            public Handler(CoursesOnLineContext  context, UserManager<User>  userManager, IJwtGenerator jwtGenerator, IPasswordHasher<User> passwordHasher)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserData> Handle(Eject request, CancellationToken cancellationToken)
            {
                var userIden = await _userManager.FindByNameAsync(request.UserName);
                if (userIden == null)
                {
                    throw new HandlerException(HttpStatusCode.NotFound, new { messaje = "UserName  Not Exist!!" });
                }

                 var result=  await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.UserName).AnyAsync();.
                if (result)
                {
                    throw new HandlerException(HttpStatusCode.InternalServerError, new { messaje = "Email other user" });
                }

                userIden.Name = request.Name + " " + request.LastNames;
                userIden.PasswordHash = _passwordHasher.HashPassword(userIden, request.Password);
                userIden.Email = request.Email;

                var resultUpdate = await _userManager.UpdateAsync(userIden);

                var resultadoRoles = await _userManager.GetRolesAsync(userIden);
                var listRoles = new List<string>(resultadoRoles);

                if(resultUpdate.Succeeded)
                {
                    return new UserData
                    {
                        Name = userIden.Name,
                        UserName = userIden.UserName,
                        Email = userIden.Email,
                        Token = _jwtGenerator.CreateToken(userIden, listRoles)
                    };
                }

                throw new Exception("User Not Update");


            }
        }


    }
}
