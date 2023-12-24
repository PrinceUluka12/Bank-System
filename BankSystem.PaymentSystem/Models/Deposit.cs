using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.PaymentSystem.Models
{
    public class Deposit
    {
        [Key]
        public int DepositId { get; set; }
        public int CustomerId { get; set; }
        public int AccountNumber { get; set; }
        public string DepositorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        [ForeignKey("AccountNumber")]
        public AccountInfo AccountDetails { get; set; }
    }
}
