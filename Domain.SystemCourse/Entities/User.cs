using Microsoft.AspNetCore.Identity;

namespace Domain.SystemCourse.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}