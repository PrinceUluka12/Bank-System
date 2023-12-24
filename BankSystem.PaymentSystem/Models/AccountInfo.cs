using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.PaymentSystem.Models
{
    public class AccountInfo
    {
        public int AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }

        // Navigation property
        [ForeignKey(nameof(CustomerId))]
        public CustomerInfo Customer { get; set; }
    }
}
