using System.Threading;
using System.Threading.Tasks;
using Application.SystemCourse.Contracts;
using Domain.SystemCourse.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.SystemCourse.Security
{
    public class UserActual
    {
        public class Eject : IRequest<UserData>{}

        public class Handler: IRequestHandler<Eject, UserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserSession _userSession;

            public Handler(UserManager<User> userManager, IJwtGenerator jwtGenerator, IUserSession userSession)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _userSession = userSession;
            }

            public async Task<UserData> Handle(Eject request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userSession.ObtainUserSession());

                return new UserData
                {
                    Name= user.Name,
                    UserName= user.UserName,
                    Token= _jwtGenerator.CreateToken(user),
                    Image=null,
                    Email=user.Email
                };
            }
        }
    }
}