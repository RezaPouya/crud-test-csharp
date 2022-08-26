namespace Mc2.CrudTest.AcceptanceTests.Models
{
    [Serializable]
    public  class ApiResult
    {
        public bool IsSuccess { get; set; }
        public string StatusCode { get; set; }

        public string? ErrorCode { get; set; }
        public Dictionary<string, string>? Errors { get; set; }

        public string? Message { get; set; }
    }

    [Serializable]
    public class ApiResult<TData> : ApiResult 
    {
        public TData? Data { get; set; }
    }
}