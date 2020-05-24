using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.SystemCourse.Contracts;
using Application.SystemCourse.HandlerError;
using Domain.SystemCourse.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.SystemCourse;

namespace Application.SystemCourse.Security
{
    public class Register
    {
        public class Eject : IRequest<UserData>{
             public string Name { get; set; }
             public string LastNames { get; set; }
             public string Email { get; set; }
             public string Password { get; set; }
             public string UserName {get;set;}
        }

        public class EjectValidator : AbstractValidator<Eject>
        {
            public EjectValidator(){
                RuleFor(x=>x.Name).NotEmpty();
                RuleFor(x=>x.LastNames).NotEmpty();
                RuleFor(x=>x.Email).NotEmpty();
                RuleFor(x=>x.Password).NotEmpty();
                RuleFor(x=>x.UserName).NotEmpty();

            }
        }

        public class Handler : IRequestHandler<Eject, UserData>
        {
            private readonly CoursesOnLineContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(CoursesOnLineContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }
            public async Task<UserData> Handle(Eject request, CancellationToken cancellationToken)
            {
                var exist= await _context.Users.Where(x=>x.Email== request.Email).AnyAsync();
                if(exist)
                {
                    throw new HandlerException(HttpStatusCode.BadRequest, new {messaje="Email exist!!"});
                }

                var existUser= await _context.Users.Where(x=>x.UserName==request.UserName).AnyAsync();
                if(existUser)
                {
                  throw new HandlerException(HttpStatusCode.BadRequest, new {messaje="UserName exist!!"});
                }

                var user = new User
                {
                    Name =$"{request.Name} {request.LastNames}",
                    Email= request.Email,
                    UserName= request.Email
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if(result.Succeeded)
                {
                    return new UserData
                    {
                        Name= user.Name,
                        Token= _jwtGenerator.CreateToken(user),
                        UserName= user.UserName,
                        Email= user.Email
                    };
                }

                throw new Exception("Not cant create the new user");


            }
        }
    }
}