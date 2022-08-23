using Mc2.CrudTest.Domain.BaseEntities;

namespace Mc2.CrudTest.Domain.Customers
{
    public class CustomerException : BusinessException
    {
        public CustomerException(): base()
        {
        }
        public CustomerException(string message) : base(message)
        {
        }

        public CustomerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }




    }
}