using BankSystem.PaymentSystem.Models;
using BankSystem.PaymentSystem.Models.DTO;
using BankSystem.PaymentSystem.Services.Interface;
using Newtonsoft.Json;
using System.Net.Http;

namespace BankSystem.PaymentSystem.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private IHttpClientFactory _httpClientFactory;

        public CustomerService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<CustomerInfo> GetCustomerAccountInfo(int accountNumber)
        {
            var client = _httpClientFactory.CreateClient("Customer");
            var response = await client.GetAsync($"/api/CustomerAccount/GetCustomerAccountInfoByAccountNumber/{accountNumber}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                var data =  JsonConvert.DeserializeObject<dynamic>(Convert.ToString(resp.Result));
                CustomerInfo customerInfo = new CustomerInfo()
                {
                    CustomerId = data.CustomerId,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName
                };
                return customerInfo;
            }
            return new CustomerInfo();
        }
    }
}
