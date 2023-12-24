using BankSystem.PaymentSystem.Data;
using BankSystem.PaymentSystem.Models;
using BankSystem.PaymentSystem.Models.DTO;
using BankSystem.PaymentSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BankSystem.PaymentSystem.Services.Implementation
{
    public class DepositService : IDepositService
    {
        private readonly AppDbContext _db;
        private readonly ICustomerService _customer;
        private IHttpClientFactory _httpClientFactory;

        public DepositService(AppDbContext db, ICustomerService customer, IHttpClientFactory clientFactory)
        {
           _db = db;
            _httpClientFactory = clientFactory;
            _customer = customer;
        }
        public async Task<List<Deposit>> GetDepositsByCustomerId(int accountNumber)
        {
            try
            {
                var result = await _db.Deposits.Where(e => e.AccountNumber == accountNumber).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<Deposit>> GetDepositsByCustomerIdAndDate(int accountNumber, DateTime From, DateTime To)
        {
            try
            {
                var result = await _db.Deposits.Where(e => e.AccountNumber == accountNumber && e.TimeStamp >= From && e.TimeStamp <= To).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<bool> MakeDeposit(Deposit model)
        {
            try
            {
                if(await ConfirmCustomerDetails(model))
                {
                    var client = _httpClientFactory.CreateClient("Deposit");
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"/api/CustomerAccount/UpdateAccountBalance",content);
                    var apiContent = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    if (resp.IsSuccess)
                    {
                        return JsonConvert.DeserializeObject<bool>(Convert.ToString(resp.Result));
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<bool> ConfirmCustomerDetails(Deposit model)
        {
            try
            {
                var data = await _customer.GetCustomerAccountInfo(model.AccountNumber);
                if (data.FirstName == model.FirstName && data.LastName == model.LastName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
