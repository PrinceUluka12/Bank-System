using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.PaymentSystem.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int SenderAccountNumber { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
    }
}
