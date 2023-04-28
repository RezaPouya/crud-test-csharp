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
        public string Description { get; set; }

        public BusinessException WithErrorCode(string code)
        {
            this.Code = code;
            this.Description = ErrorMessages.GetMessage(code);
            return this;
        }

        public BusinessException WithErrorCode(string code , string desc)
        {
            this.Code = code;
            this.Description = desc;
            return this;
        }
    }
}