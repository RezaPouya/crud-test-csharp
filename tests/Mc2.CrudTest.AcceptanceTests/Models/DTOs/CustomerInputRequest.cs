using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.AcceptanceTests.Models.DTOs
{
    [Serializable]
    public class CustomerInputRequest
    {
        public CustomerInputRequest()
        {

        }

        public CustomerInputRequest(CustomerOutputResponse model)
        {
            this.Id = model.Id;
            this.PhoneNumber = $"+{model.PhoneNumber}" ;
            this.DateOfBirth = model.DateOfBirth.ToString("dd-MMM-yyyy");
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.Email = model.Email;
            this.BankAccountNumber = model.BankAccountNumber;
        }

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