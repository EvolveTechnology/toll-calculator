using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Toll.Calculator.UnitTests.Common
{
    public abstract class ControllerUnitTest<T> : UnitTestBase<T>
    {
        protected override void AddCustomizations(Fixture fixture)
        {
            base.AddCustomizations(fixture);

            fixture.Customize(new ControllerCustomization());
        }

        public bool HasAuthorizeAttribute(Expression<Action> expression, string policy = null)
        {
            var method = MethodOf(expression);

            return MethodHasAuthorizePolicy(method, policy);
        }

        private static bool MethodHasAuthorizePolicy(MethodInfo method, string policy = null)
        {
            if (policy is null)
            {
                return method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();
            }

            return method.GetCustomAttributesData()
                .Where(x => x.AttributeType == typeof(AuthorizeAttribute))
                .Select(y => y.NamedArguments != null && y.NamedArguments
                                 .Any(z => (string)z.TypedValue.Value == policy))
                .FirstOrDefault();
        }

        private static MethodInfo MethodOf(Expression<Action> expression)
        {
            var body = (MethodCallExpression)expression.Body;

            return body.Method;
        }
    }

    internal class ControllerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new FilteringSpecimenBuilder(
                    new Postprocessor(
                        new MethodInvoker(
                            new ModestConstructorQuery()),
                        new ControllerFiller()),
                    new ControllerSpecification()));
        }


        private class ControllerFiller : ISpecimenCommand
        {
            public void Execute(object specimen, ISpecimenContext context)
            {
                if (specimen is null)
                {
                    throw new ArgumentNullException(nameof(specimen));
                }

                if (context is null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                var target = specimen as Controller;

                if (target is null)
                {
                    throw new ArgumentException("The specimen must be an instance of Controller.", nameof(specimen));
                }

                var httpContext = new DefaultHttpContext
                {
                    User = new GenericPrincipal(new GenericIdentity("test"), null)
                };

                target.TempData = new TempDataDictionary(httpContext, new SessionStateTempDataProvider());

                target.ControllerContext.HttpContext = httpContext;
            }
        }

        private class ControllerSpecification : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                var requestType = request as Type;

                return requestType != null && typeof(Controller).IsAssignableFrom(requestType);
            }
        }
    }
}