using System;
using System.Net;

namespace Application.SystemCourse.HandlerError
{
    public class HandlerException : Exception
    {
        public HttpStatusCode Code { get;  }
        public object Errors {get;}
        public HandlerException(HttpStatusCode code, object errors=null)
        {
            Code = code;
            Errors= errors;
        }
    }
}