using System.Linq;
using System.Threading.Tasks;
using Domain.SystemCourse.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.SystemCourse
{
    public class DataTest
    {
        public static async  Task InsertData(CoursesOnLineContext context, UserManager<User> userManager){
                if(!userManager.Users.Any()){
                    var user= new User{Name="Raolis", UserName="rmendozab", Email="raolis1989@gmail.com"};
                    await userManager.CreateAsync(user,"Password12345.");
                }
        }
    }
}