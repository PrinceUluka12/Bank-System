using BankSystem.CustomerInformation.Data;
using BankSystem.CustomerInformation.Models;
using BankSystem.CustomerInformation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BankSystem.CustomerInformation.Services.Implementations
{
    public class CustomerInfoService : ICustomerInfoService
    {

        private readonly AppDbContext _db;
        private readonly IAccountInfoService _accountInfoService;
        private readonly IEmailService _emailService;
        public CustomerInfoService(AppDbContext db, IAccountInfoService accountInfoService, IEmailService emailService)
        {
            _accountInfoService = accountInfoService;
            _db = db;   
            _emailService = emailService;
        }
        public async Task<bool> AddNewCustomer(AccountCreationDTO info)
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
                    OpenDate =  DateTime.Now,
                    
                };

                if (await _accountInfoService.AddNewCustomerAccount(accountInfos))
                {
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

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
               var customer  = await _db.CustomerInfo.FirstOrDefaultAsync(c => c.CustomerID == id);
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

        public async Task<List<CustomerInfos>> GetAll()
        {
            try
            {
                var customers =  await _db.CustomerInfo.ToListAsync();
                return customers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CustomerInfos> GetCustomerById(int id)
        {
            try
            {
                var customer =  await _db.CustomerInfo.FirstOrDefaultAsync(c => c.CustomerID == id);
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

        
    }
}
