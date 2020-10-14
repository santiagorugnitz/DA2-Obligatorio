using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int code = 500;
            if (context.Exception is NotFoundException) code = 404;
            else if (context.Exception is BadRequestException) code = 400;
            
            context.Result = new ContentResult()
            {
                StatusCode=code,
                Content = context.Exception.Message
            };
        }
    }
}
