using Mc2.CrudTest.HttpApi.Host.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mc2.CrudTest.WebUi.Filters
{
    /// <summary>
    /// https://stackoverflow.com/questions/9381520/what-is-the-appropriate-http-status-code-response-for-a-general-unsuccessful-req
    /// </summary>
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                OkObjectResult(context, okObjectResult);
                base.OnResultExecuting(context);
                return;
            }

            if (context.Result is OkResult okResult)
            {
                OkResult(context, okResult);
                base.OnResultExecuting(context);
                return;
            }

            if (context.Result is BadRequestResult badRequestResult)
            {
                BadRequestResult(context, badRequestResult);
                base.OnResultExecuting(context);
                return;
            }

            if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                BadRequestObjectResult(context, badRequestObjectResult);
                base.OnResultExecuting(context);
                return;
            }

            if (context.Result is ContentResult contentResult)
            {
                ContentResult(context, contentResult);
                base.OnResultExecuting(context);
                return;
            }

            if (context.Result is NotFoundResult notFoundResult)
            {
                NotFoundResult(context, notFoundResult);
                base.OnResultExecuting(context);
                return;
            }

            if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                NotFoundObjectResult(context, notFoundObjectResult);
                base.OnResultExecuting(context);
                return;
            }

            if (context.Result is ObjectResult objectResult &&
                objectResult.StatusCode == null &&
                !(objectResult.Value is ApiResult))
            {
                if (objectResult.Value is null)
                {
                    NotFoundObjectResult(context, objectResult);
                    base.OnResultExecuting(context);
                    return;
                }
                else
                {
                    OkObjectResult(context, objectResult);
                    base.OnResultExecuting(context);
                    return;
                }
            }

            if (context.Result is ObjectResult objectResult2 &&
                objectResult2.StatusCode == 200 &&
                !(objectResult2.Value is ApiResult))
            {
                context.Result =
                    new JsonResult(new ApiResult<object>(objectResult2.Value))
                    {
                        StatusCode = objectResult2.StatusCode
                    };
            }

            base.OnResultExecuting(context);
        }

        private static void OkResult(ResultExecutingContext context, OkResult okResult)
        {
            context.Result = new JsonResult(new ApiResult()) { StatusCode = okResult.StatusCode };
        }

        private void OkObjectResult(ResultExecutingContext context, OkObjectResult okObjectResult)
        {
            context.Result =
                new JsonResult(new ApiResult<object>(okObjectResult.Value)) { StatusCode = okObjectResult.StatusCode };
        }

        private void OkObjectResult(ResultExecutingContext context, ObjectResult okObjectResult)
        {
            var apiResult = new ApiResult<object>(okObjectResult.Value);
            context.Result =
                new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
        }

        private static void BadRequestResult(ResultExecutingContext context, BadRequestResult badRequestResult)
        {
            var apiResult = new ApiResult(statusCode: "400", errorCode: null, "Bad request", null);
            context.Result = new JsonResult(apiResult) { StatusCode = badRequestResult.StatusCode };
        }

        private static void BadRequestObjectResult(ResultExecutingContext context, BadRequestObjectResult badRequestObjectResult)
        {
            string? message = badRequestObjectResult?.Value?.ToString() ?? null;

            ApiResult<object> result;

            if (badRequestObjectResult?.Value is SerializableError errors)
            {
                var e = new Dictionary<string, string>();

                foreach (var error in errors.Distinct())
                    e.Add(error.Key, error.Value.ToString() ?? "");

                result = new ApiResult<object>(statusCode: badRequestObjectResult?.StatusCode.ToString() ?? "400", errorCode: null, message: message ?? "Bad Request", null, e);
            }
            else
            {
                result = new ApiResult<object>( statusCode: badRequestObjectResult?.StatusCode.ToString() ?? "400", errorCode: null , message: message ?? "Bad Request", null, null);
            }

            context.Result = new JsonResult(result) { StatusCode = badRequestObjectResult.StatusCode };
        }

        private static void ContentResult(ResultExecutingContext context, ContentResult contentResult)
        {
            var apiResult = 
            context.Result = new JsonResult(new ApiResult(contentResult?.Content ?? "")) { StatusCode = contentResult.StatusCode };
        }

        private static void NotFoundResult(ResultExecutingContext context, NotFoundResult notFoundResult)
        {
            var apiResult = new ApiResult<object>(statusCode: "404", errorCode: null, "Not found", null);
            context.Result = new JsonResult(apiResult) { StatusCode = notFoundResult.StatusCode };
        }

        private static void NotFoundObjectResult(ResultExecutingContext context, NotFoundObjectResult notFoundObjectResult)
        {
            var apiResult = new ApiResult<object>(statusCode: "404", errorCode: null ,  "Not found", notFoundObjectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
        }

        private static void NotFoundObjectResult(ResultExecutingContext context, ObjectResult notFoundObjectResult)
        {
            var apiResult = new ApiResult<object>(statusCode: "404", errorCode: null, "Not found", notFoundObjectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
        }
    }
}