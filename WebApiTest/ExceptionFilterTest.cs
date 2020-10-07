using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Abstractions;
using WebApi.Filters;
namespace WebApiTest
{
    [TestClass]
    public class ExceptionFilterTest
    {
        [TestMethod]
        public void Test500()
        {
            ExceptionFilter filter = new ExceptionFilter();


            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();

            var context = new ExceptionContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());
            context.Exception = new InvalidOperationException();
            filter.OnException(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(500, response.StatusCode);
        }
        [TestMethod]
        public void Test400()
        {
            ExceptionFilter filter = new ExceptionFilter();


            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();

            var context = new ExceptionContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());
            context.Exception = new ArgumentException();
            filter.OnException(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(400, response.StatusCode);
        }
    }
}
