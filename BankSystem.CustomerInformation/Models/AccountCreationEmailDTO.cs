namespace BankSystem.CustomerInformation.Models
{
    public class AccountCreationEmailDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        public int AccountNumber { get; set; }
        public int CustomerID { get; set; }
    }
}
