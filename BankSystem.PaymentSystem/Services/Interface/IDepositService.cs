using BankSystem.PaymentSystem.Models;

namespace BankSystem.PaymentSystem.Services.Interface
{
    public interface IDepositService
    {
        Task<bool> MakeDeposit(Deposit model);
        Task<List<Deposit>> GetDepositsByCustomerId(int customerId);
        Task<List<Deposit>> GetDepositsByCustomerIdAndDate(int customerId, DateTime From,DateTime To);
    }
}
