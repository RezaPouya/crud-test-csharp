namespace Mc2.CrudTest.Domain.BaseEntities
{
    public class BusinessException : Exception
    {
        public BusinessException() : base()
        {
        }
        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public string Code { get; set; }
        public new string Message { get; set; }

        public void SetMessage(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public void SetErrorCode(string code)
        {
            this.Code = code;
            this.Message = ErrorMessages.GetMessage(code);
        }

        public BusinessException WithErrorCode(string code)
        {
            this.Code = code;
            this.Message = ErrorMessages.GetMessage(code);
            return this;
        }
    }
}