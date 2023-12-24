using BankSystem.CustomerInformation.Models;
using BankSystem.CustomerInformation.Models.DTO;
using BankSystem.CustomerInformation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankSystem.CustomerInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {

        private readonly IAccountInfoService _accountInfoService;
        private ResponseDto _response;
        public CustomerAccountController(IAccountInfoService accountInfoService)
        {
            _accountInfoService = accountInfoService;
            _response = new ResponseDto();
        }

        [HttpPost("AddNewCustomer")]
        public async Task<ResponseDto> AddNewCustomer(AccountCreationDTO account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.Message = "ERROR.....";
                    return _response;
                }
                else
                {
                    bool response = await _accountInfoService.AddNewCustomer(account);
                    _response.Result = response;
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }

        }

        //[HttpGet("GetCustomers")]
        //public async Task<IActionResult> GetCustomers()
        //{
        //    try
        //    {
        //        var data = await _accountInfoService.GetAll();
        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("GetCustomerById/{customerId:int}")]
        public async Task<ResponseDto> GetCustomerById(int customerId)
        {
            try
            {
                var data = await _accountInfoService.GetCustomerById(customerId);
                _response.Result = data;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }

        [HttpGet("GetCustomerAccountInfoByCustomerId/{customerId:int}")]
        public async Task<ResponseDto> GetCustomerAccountInfoByCustomerId(int customerId)
        {
            try
            {
                var data = await _accountInfoService.GetCustomerAccountInfoByCustomerId(customerId);
                _response.Result = data;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }

        [HttpGet("GetCustomerAccountInfoByAccountNumber/{AccountNumber:int}")]
        public async Task<ResponseDto> GetCustomerAccountInfoByAccountNumber(int AccountNumber)
        {
            try
            {
                var data = await _accountInfoService.GetCustomerAccountInfoByAccountNumber(AccountNumber);
                _response.Result = data;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }

        [HttpPut("UpdateCustomerInfo")]
        public async Task<ResponseDto> UpdateCustomerInfo(CustomerInfos info)
        {
            try
            {
                var data = await _accountInfoService.UpdateCustomer(info);
                _response.Result = data;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }

        [HttpDelete("DeleteCustomerInfo/{CustomerId:int}")]
        public async Task<ResponseDto> DeleteCustomerInfo(int CustomerId)
        {
            try
            {
                var data = await _accountInfoService.DeleteCustomer(CustomerId);
                _response.Result = data;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }

        [HttpPost("UpdateAccountBalance")]
        public async Task<ResponseDto> UpdateAccountBalance([FromBody] DepositDTO model)
        {
            try
            {
                var data = await _accountInfoService.UpdateAccountBalance(model.AccountNumber, model.Balance);
                _response.Result = data;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }
    }
}
