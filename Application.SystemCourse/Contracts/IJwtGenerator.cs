using Domain.SystemCourse.Entities;

namespace Application.SystemCourse.Contracts
{
    public interface IJwtGenerator
    {
         string CreateToken(User user);
    }
}