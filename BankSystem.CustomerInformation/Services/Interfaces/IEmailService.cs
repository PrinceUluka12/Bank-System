using BankSystem.CustomerInformation.Models;

namespace BankSystem.CustomerInformation.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> AccountCreationMail(AccountCreationEmailDTO model);
    }
}
