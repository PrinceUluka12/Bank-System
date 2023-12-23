using BankSystem.CustomerInformation.Models;

namespace BankSystem.CustomerInformation.Services.Interfaces
{
    public interface ICustomerInfoService
    {
        Task<bool> AddNewCustomer(AccountCreationDTO info);
        Task<bool> DeleteCustomer(int id);
        Task<bool> UpdateCustomer(CustomerInfos info);
        Task<List<CustomerInfos>> GetAll();
        Task<CustomerInfos> GetCustomerById(int id);
    }
}
