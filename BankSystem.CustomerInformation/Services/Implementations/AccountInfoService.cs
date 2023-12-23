using BankSystem.CustomerInformation.Data;
using BankSystem.CustomerInformation.Models;
using BankSystem.CustomerInformation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.CustomerInformation.Services.Implementations
{
    public class AccountInfoService : IAccountInfoService
    {
        private readonly AppDbContext _db;
        
        public AccountInfoService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddNewCustomerAccount(AccountInfos info)
        {
            try
            {
                _db.AccountInfo.Add(info);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAccount(int accoutNumber)
        {
            try
            {
                var account = await _db.AccountInfo.FirstOrDefaultAsync(a => a.AccountNumber == accoutNumber);
                if (account == null)
                {
                    return false;
                }
                else
                {
                    _db.AccountInfo.Remove(account);
                    await _db.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<AccountInfos> GetAccountById(int id)
        {
            try
            {
                var account = await _db.AccountInfo.FirstOrDefaultAsync(c => c.AccountNumber == id);
                if (account == null)
                {
                    return null;
                }
                else
                {
                    return account;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AccountInfos> GetCustomerAccountInfoByCustomerId(int id)
        {
            try
            {
                var account = await _db.AccountInfo.FirstOrDefaultAsync(c => c.CustomerID == id);
                if (account == null)
                {
                    return null;
                }
                else
                {
                    return account;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateCustomerAccount(AccountInfos info)
        {
            try
            {
                var account = await _db.AccountInfo.AsNoTracking().FirstOrDefaultAsync(c => c.AccountNumber == info.AccountNumber);
                if (account == null)
                {
                    return false;
                }
                else
                {
                    _db.Entry(account).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
