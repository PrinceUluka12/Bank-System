using BankSystem.CustomerInformation.Models;

namespace BankSystem.CustomerInformation.Services.Interfaces
{
    public interface IAccountInfoService
    {
        Task<bool> AddNewCustomerAccount(AccountCreationDTO info);
        Task<bool> DeleteCustomerAccount(int accountNumber);
        Task<bool> UpdateCustomerAccount(AccountInfos info);
        //Task<List<CustomerInfo>> GetAll();
        Task<AccountInfos> GetAccountById(int id);
        Task<AccountInfos> GetCustomerAccountInfoByCustomerId(int id);

        Task<AccountInfos> GetCustomerAccountInfoByAccountNumber(int AccountNumber);

        Task<bool> AddNewCustomer(AccountCreationDTO info);
        Task<bool> DeleteCustomer(int id);
        Task<bool> UpdateCustomer(CustomerInfos info);
        Task<List<CustomerInfos>> GetAllCustomerInfosAndAccount();
        Task<CustomerInfos> GetCustomerById(int id);
        Task<bool>UpdateAccountBalance(int accountNumber, double balance); 
    }
}
