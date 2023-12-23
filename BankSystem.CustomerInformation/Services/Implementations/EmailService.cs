using BankSystem.CustomerInformation.Models;
using BankSystem.CustomerInformation.Services.Interfaces;

namespace BankSystem.CustomerInformation.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IPublishMessages _publish;
        public EmailService(IPublishMessages publish)
        {
            _publish = publish;
        }
        public async Task<bool> AccountCreationMail(AccountCreationEmailDTO model)
        {
            Notifications notifications = new Notifications();
            notifications.Subject = "Welcome to Prince Bank - Your Account Has Been Created!";
            notifications.Body = EmailFormats.AccountCreationMail.Replace("[CustomerName]" , model.FirstName + " " + model.LastName)
                .Replace("[CustomerEmail]",model.Email)
                .Replace("[AccountID]", model.AccountNumber.ToString())
                .Replace("[CustomerID]",model.CustomerID.ToString());
            notifications.To = model.Email;

             _publish.SendEmail(notifications);

            return true;
        }
    }
}
