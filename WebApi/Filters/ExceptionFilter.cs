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
            if (context.Exception is NullReferenceException) code = 404;
            else if (context.Exception is ArgumentException || context.Exception is InvalidOperationException) code = 400;
            
            context.Result = new ContentResult()
            {
                StatusCode=code,
                Content = context.Exception.Message
            };
        }
    }
}
