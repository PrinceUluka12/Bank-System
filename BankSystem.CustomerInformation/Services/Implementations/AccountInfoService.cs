using BankSystem.CustomerInformation.Data;
using BankSystem.CustomerInformation.Models;
using BankSystem.CustomerInformation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.CustomerInformation.Services.Implementations
{
    public class AccountInfoService : IAccountInfoService
    {
        private readonly AppDbContext _db;
       
        private readonly IEmailService _emailService;

        public AccountInfoService(AppDbContext db, IEmailService emailService)
        {
            _db = db;
            
            _emailService = emailService;
        }

        public Task<bool> AddNewCustomer(AccountCreationDTO info)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddNewCustomerAccount(AccountCreationDTO info)
        {
            try
            {
                CustomerInfos customerInfos = new CustomerInfos
                {
                    Address = info.Address,
                    DateOfBirth = info.DateOfBirth,
                    Email = info.Email,
                    FirstName = info.FirstName,
                    LastName = info.LastName,
                    Gender = info.Gender,
                    PhoneNumber = info.PhoneNumber
                };
                _db.CustomerInfo.Add(customerInfos);
                await _db.SaveChangesAsync();

                AccountInfos accountInfos = new AccountInfos
                {
                    AccountType = info.AccountType,
                    Balance = 0,
                    CustomerID = customerInfos.CustomerID,
                    OpenDate = DateTime.Now,

                };
                _db.AccountInfo.Add(accountInfos);
                await _db.SaveChangesAsync();

                AccountCreationEmailDTO emailDTO = new AccountCreationEmailDTO()
                {
                    AccountNumber = accountInfos.AccountNumber,
                    CustomerID = customerInfos.CustomerID,
                    AccountType = accountInfos.AccountType,
                    Email = customerInfos.Email,
                    FirstName = customerInfos.FirstName,
                    LastName = customerInfos.LastName
                };
                await _emailService.AccountCreationMail(emailDTO);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _db.CustomerInfo.FirstOrDefaultAsync(c => c.CustomerID == id);
                if (customer == null)
                {
                    return false;
                }
                else
                {
                    _db.CustomerInfo.Remove(customer);
                    await _db.SaveChangesAsync();

                    var accountInfo = await _db.AccountInfo.FirstOrDefaultAsync(u => u.CustomerID == customer.CustomerID);
                    if (accountInfo == null) { }
                    else
                    {
                        _db.AccountInfo.Remove(accountInfo);
                        await _db.SaveChangesAsync();
                    }
                    return true;
                }

            }
            catch (Exception ex)
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

        public Task<List<CustomerInfos>> GetAllCustomerInfosAndAccount()
        {
            throw new NotImplementedException();
        }

        public async Task<AccountInfos> GetCustomerAccountInfoByAccountNumber(int AccountNumber)
        {
            try
            {
                var account = await _db.AccountInfo.FirstOrDefaultAsync(c => c.AccountNumber == AccountNumber);
                if (account == null)
                {
                    return null;
                }
                else
                {
                    account.Customer = await _db.CustomerInfo.FirstOrDefaultAsync(u => u.CustomerID == account.CustomerID);
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
                    account.Customer = await _db.CustomerInfo.FirstOrDefaultAsync(u => u.CustomerID == account.CustomerID);
                    return account;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CustomerInfos> GetCustomerById(int id)
        {
            try
            {
                var customer = await _db.CustomerInfo.FirstOrDefaultAsync(c => c.CustomerID == id);
                if (customer == null)
                {
                    return null;
                }
                else
                {
                    return customer;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAccountBalance(int accountNumber, double balance)
        {
            try
            {
                var account = await _db.AccountInfo.AsNoTracking().FirstOrDefaultAsync(c => c.AccountNumber == accountNumber);
                if (account == null)
                {
                    return false;
                }
                else
                {
                    account.Balance = balance + account.Balance;
                    _db.Entry(account).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCustomer(CustomerInfos info)
        {
            try
            {
                var customer = await _db.CustomerInfo.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerID == info.CustomerID);
                if (customer == null)
                {
                    return false;
                }
                else
                {
                    _db.Entry(customer).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
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
