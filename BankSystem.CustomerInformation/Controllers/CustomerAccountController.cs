using BankSystem.CustomerInformation.Models;
using BankSystem.CustomerInformation.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.CustomerInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerInfoService _customerInfoService;
        private readonly IAccountInfoService _accountInfoService;
        public CustomerAccountController(ICustomerInfoService customerInfoService, IAccountInfoService accountInfoService)
        {
            _accountInfoService = accountInfoService;
            _customerInfoService = customerInfoService;
        }

        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddNewCustomer(AccountCreationDTO account) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    bool response = await _customerInfoService.AddNewCustomer(account);
                    return Ok(response);
                } 
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var data =await _customerInfoService.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerById/{customerId}")]
        public async Task<IActionResult> GetCustomerById(int CustomerId)
        {
            try
            {
                var data = await _customerInfoService.GetCustomerById(CustomerId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerAccountInfoByCustomerId/{customerId}")]
        public async Task<IActionResult> GetCustomerAccountInfoByCustomerId(int CustomerId)
        {
            try
            {
                var data = await _accountInfoService.GetCustomerAccountInfoByCustomerId(CustomerId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCustomerInfo")]
        public async Task<IActionResult> UpdateCustomerInfo(CustomerInfos info)
        {
            try
            {
                var data = await _customerInfoService.UpdateCustomer(info);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCustomerInfo/{CustomerId}")]
        public async Task<IActionResult> DeleteCustomerInfo(int CustomerId)
        {
            try
            {
                var data = await _customerInfoService.DeleteCustomer(CustomerId);
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
