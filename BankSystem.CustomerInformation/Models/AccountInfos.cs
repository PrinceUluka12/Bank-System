using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.CustomerInformation.Models
{
    public class AccountInfos
    {
        [Key]
        public int AccountNumber { get; set; }
        public int CustomerID { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public DateTime? OpenDate { get; set; }
        [ForeignKey("CustomerID")]
        public CustomerInfos Customer { get; set; }
    }
}
