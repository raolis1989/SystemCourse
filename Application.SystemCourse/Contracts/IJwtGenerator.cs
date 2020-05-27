using Domain.SystemCourse.Entities;
using System.Collections.Generic;

namespace Application.SystemCourse.Contracts
{
    public interface IJwtGenerator
    {
         string CreateToken(User user,  List<string> roles);
    }
}