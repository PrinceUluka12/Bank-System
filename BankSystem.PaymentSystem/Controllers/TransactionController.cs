using BankSystem.PaymentSystem.Models;
using BankSystem.PaymentSystem.Models.DTO;
using BankSystem.PaymentSystem.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IDepositService _depositService;
        private ResponseDto _response;
        public TransactionController(IDepositService depositService)
        {
            _depositService = depositService;
            _response = new ResponseDto();
        }

        [HttpPost("MakeDeposit")]
        public async Task<ResponseDto> MakeDeposit([FromBody]Deposit model)
        {
            try
            {
                var data =await _depositService.MakeDeposit(model);
                _response.Result = data;
                return _response;
            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return _response;
            }
        }
    }
}
