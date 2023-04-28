using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Mc2.CrudTest.HttpApi.Host.Models
{

    [Serializable]
    public class ApiResult<TData> : ApiResult
        where TData : class
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public new string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public new Dictionary<string, string>? Errors { get; set; }

        public ApiResult(TData? data)
        {
            IsSuccess = true;
            Data = data;
        }

        public ApiResult(string message, TData? data)
        {
            IsSuccess = true;
            StatusCode = "200"; 
            Message = message;
            Data = data;
        }

        public ApiResult(string statusCode, string? errorCode = null , string? message = null, TData? data = null, Dictionary<string, string>? erros = null)
        {
            IsSuccess = false;
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Data = data;
            Message = message;
            Errors = erros is null ? null : erros;
        }

        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(data);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            if (result is null)
                return new ApiResult<TData>(null);

            return new ApiResult<TData>(result?.Value as TData);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>("400", null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            string? message = result?.Value?.ToString() ?? null;

            if (result?.Value is SerializableError errors)
            {
                var e = new Dictionary<string, string>();

                foreach (var error in errors.Distinct())
                    e.Add(error.Key, error.Value.ToString() ?? "");

                return new ApiResult<TData>(statusCode: result?.StatusCode.ToString() ?? "400", errorCode: "" ,   message ?? "Bad Request", null, e);
            }

            return new ApiResult<TData>(statusCode: result?.StatusCode.ToString() ?? "400", errorCode: "", message ?? "Bad Request", null, null);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            return new ApiResult<TData>(result.Content ?? "", null);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>("404", "Not found", null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(statusCode: "404", errorCode: null , message: "Not found", data: (TData)result.Value);
        }
    }
}