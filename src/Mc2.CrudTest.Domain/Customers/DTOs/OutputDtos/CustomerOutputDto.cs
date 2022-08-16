using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos
{
    [Serializable]
    public class CustomerOutputDto
    {
        [Key]
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}