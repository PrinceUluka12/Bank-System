using BankSystem.PaymentSystem.Models;

namespace BankSystem.PaymentSystem.Services.Interface
{
    public interface ICustomerService
    {
        Task<CustomerInfo> GetCustomerAccountInfo(int accountNumber);
    }
}
