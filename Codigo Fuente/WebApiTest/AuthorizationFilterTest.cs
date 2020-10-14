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
    public class AuthorizationFilterTest
    {
        [TestMethod]
        public void TestAuthFilterWithoutHeader()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            AuthorizationFilter authFilter = new AuthorizationFilter(mock.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(401, response.StatusCode);
        }

        [TestMethod]
        public void TestAuthFilterWithValidHeader()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            AuthorizationFilter authFilter = new AuthorizationFilter(mock.Object);

            string token = "123qweasdzxc";

            mock.Setup(x => x.IsLogged(token)).Returns(true);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["token"] = "123qweasdzxc";
            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.IsNull(response);
        }

        [TestMethod]
        public void TestAuthFilterWithInvalidHeader()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            AuthorizationFilter authFilter = new AuthorizationFilter(mock.Object);

            string token = "123qweasdzxc";

            mock.Setup(x => x.IsLogged(token)).Returns(false);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["token"] = "123qweasdzxc";

            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(401, response.StatusCode);
        }
    }
}
