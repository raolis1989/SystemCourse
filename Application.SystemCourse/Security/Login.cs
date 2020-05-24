using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.SystemCourse.Contracts;
using Application.SystemCourse.HandlerError;
using Domain.SystemCourse.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.SystemCourse.Security
{
    public class Login
    {
        public class Eject : IRequest<UserData>{
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class EjectValidation: AbstractValidator<Eject>{
            public EjectValidation(){
                RuleFor(x=>x.Email).NotEmpty();
                RuleFor(x=>x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Eject, UserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler (UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator){
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }
            public async Task<UserData> Handle(Eject request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user==null){
                    throw new HandlerException(HttpStatusCode.Unauthorized);
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if(result.Succeeded){
                    return new UserData {
                        Name= user.Name,
                        Token= _jwtGenerator.CreateToken(user),
                        UserName = user.UserName,
                        Email=  user.Email,
                        Image= null

                    };
                   
                }
                throw new HandlerException(HttpStatusCode.Unauthorized);
            }
        }
    }
}