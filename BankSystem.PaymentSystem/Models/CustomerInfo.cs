using System.ComponentModel.DataAnnotations;

namespace BankSystem.PaymentSystem.Models
{
    public class CustomerInfo
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
