using BankSystem.CustomerInformation.Models;

namespace BankSystem.CustomerInformation.Services.Interfaces
{
    public interface IPublishMessages
    {
         void SendEmail(Notifications notifications);
    }
}
