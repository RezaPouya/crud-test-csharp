using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.HttpApi.Host.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mc2.CrudTest.HttpApi.Host.Filters
{
    /// <summary>
    /// https://stackoverflow.com/questions/9381520/what-is-the-appropriate-http-status-code-response-for-a-general-unsuccessful-req
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(Exceptions.ValidationException), HandleValidationException },
                { typeof(BusinessException), HandleBusinessException },
                { typeof(CustomerException), HandleBusinessException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (Exceptions.ValidationException)context.Exception;

            var details = new ValidationProblemDetails(exception.Errors);

            Dictionary<string, string> errorMessages = new Dictionary<string, string>();

            foreach (var err in exception.Errors)
            {
                errorMessages.Add(err.Key, string.Join(" ", err.Value));
            }

            var apiResult = new ApiResult(
                statusCode: "400",
                errorCode: null,
                message: "One or more validation errors occured: ",
                erros: errorMessages);

            context.Result = new BadRequestObjectResult(apiResult);

            //context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleBusinessException(ExceptionContext context)
        {
            var exception = (BusinessException)context.Exception;

            var apiResult = new ApiResult(statusCode: "400" , exception.Code, exception.Description);
            //context.Result = new BadRequestObjectResult(exception);
            context.Result = new BadRequestObjectResult(apiResult);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}