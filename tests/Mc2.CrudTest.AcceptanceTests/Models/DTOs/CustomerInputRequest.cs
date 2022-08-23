using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.AcceptanceTests.Models.DTOs.InputDtos
{
    [Serializable]
    public class CustomerInputRequest
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}