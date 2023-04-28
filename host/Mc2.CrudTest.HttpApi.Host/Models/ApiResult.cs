using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Mc2.CrudTest.HttpApi.Host.Models
{
    [Serializable]
    public class ApiResult
    {
        public bool IsSuccess { get; set; }

        public string StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? Errors { get; set; }

        public ApiResult()
        {
            IsSuccess = true;
            StatusCode = "200";
            Message = null;
        }

        public ApiResult(string message)
        {
            IsSuccess = true;
            StatusCode = "200";
            Message = message;
        }

        public ApiResult(string statusCode, string? errorCode, string? message = null, Dictionary<string, string>? erros = null)
        {
            IsSuccess = false;
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Message = message;
            Errors = erros is null ? null : erros;
        }

        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult();
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(statusCode: "400", errorCode: null, message: "Bad rqeust", erros: null);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            string? message = result?.Value?.ToString() ?? null;

            if (result?.Value is SerializableError errors)
            {
                var errs = new Dictionary<string, string>();

                foreach (var error in errors.Distinct())
                    errs.Add(error.Key, error.Value.ToString() ?? "");

                return new ApiResult(
                    statusCode: result?.StatusCode.ToString() ?? "400",
                    errorCode: null,
                    message: message ?? "Bad Request",
                    erros: errs);
            }

            return new ApiResult(statusCode: result?.StatusCode.ToString() ?? "400",
                errorCode: null,
                message: message ?? "Bad Request",
                erros: null);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(result.Content ?? "");
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(statusCode: "404", errorCode: null, message: "Not found", erros: null);
        }
    }
}