namespace Mc2.CrudTest.Domain.Customers
{
    public class CustomerException : Exception
    {
        public CustomerException(string message) : base(message)
        {
        }

        public CustomerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}