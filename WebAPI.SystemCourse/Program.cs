using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.SystemCourse.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence.SystemCourse;

namespace WebAPI.SystemCourse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var  hostServer = CreateHostBuilder(args).Build();
            using ( var ambient= hostServer.Services.CreateScope()){
                var services = ambient.ServiceProvider; 
                try{
                    var userManager= services.GetRequiredService<UserManager<User>>();   
                    var context = services.GetRequiredService<CoursesOnLineContext>();
                    context.Database.Migrate();
                    DataTest.InsertData(context, userManager).Wait();

                }
                catch(Exception e){
                    var logging = services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(e, "Ocurrio un error en la migracion");
                }             
            }
            hostServer.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
