namespace Mc2.CrudTest.AcceptanceTests.Models
{
    [Serializable]
    public  class ApiResult
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string? Message { get; set; }
    }

    [Serializable]
    public class ApiResult<TData> : ApiResult 
    {
        public TData? Data { get; set; }
    }
}