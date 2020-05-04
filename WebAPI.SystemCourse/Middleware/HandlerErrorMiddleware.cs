using System;
using System.Net;
using System.Threading.Tasks;
using Application.SystemCourse.HandlerError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebAPI.SystemCourse.Middleware
{
    public class HandlerErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerErrorMiddleware> _logger;

        public HandlerErrorMiddleware(RequestDelegate next, ILogger<HandlerErrorMiddleware>logger)
        {
           _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context){
            try{
                await _next(context);
                
            }catch(Exception ex){
                await HandlerErrorMiddlewareAsync(context, ex, _logger);
            }
        }

        private async  Task HandlerErrorMiddlewareAsync(HttpContext context, Exception ex, ILogger<HandlerErrorMiddleware> logger)
        {
            object errors = null;
            switch(ex){
                case HandlerException me:
                        _logger.LogError(ex, "Handler Error");
                        errors= me.Errors;
                        context.Response.StatusCode = (int) me.Code;
                        break;
                case Exception e : 
                        _logger.LogError(ex, "Error de servidor");
                        errors= string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
            }
                context.Response.ContentType ="application/json";
                if(errors!=null){
                    var resultados = JsonConvert.SerializeObject(new {errors});
                    await context.Response.WriteAsync(resultados);
                }
           
        }
    }
}