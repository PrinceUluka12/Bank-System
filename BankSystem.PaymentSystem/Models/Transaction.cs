using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.PaymentSystem.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int SenderAccountNumber { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(SenderAccountNumber))]
        public AccountInfo SenderAccount { get; set; }
        [ForeignKey(nameof(ReceiverAccountNumber))]
        public AccountInfo ReceiverAccount { get; set; }
    }
}
