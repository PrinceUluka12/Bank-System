using BankSystem.CustomerInformation.Models;

namespace BankSystem.CustomerInformation.Services.Interfaces
{
    public interface IAccountInfoService
    {
        Task<bool> AddNewCustomerAccount(AccountInfos info);
        Task<bool> DeleteCustomerAccount(int accountNumber);
        Task<bool> UpdateCustomerAccount(AccountInfos info);
        //Task<List<CustomerInfo>> GetAll();
        Task<AccountInfos> GetAccountById(int id);
        Task<AccountInfos> GetCustomerAccountInfoByCustomerId(int id);
    }
}
