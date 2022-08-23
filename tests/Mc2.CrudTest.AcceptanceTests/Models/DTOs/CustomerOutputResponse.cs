using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.AcceptanceTests.Models.DTOs.OutputDtos
{
    [Serializable]
    public class CustomerOutputResponse
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ulong PhoneNumber { get; set; }
    }
}