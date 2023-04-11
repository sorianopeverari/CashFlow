using Microsoft.AspNetCore.Mvc;
using CashFlow.Domain.Models;
using CashFlow.Domain.Business;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CashFlowControlController : ControllerBase
    {   
        private readonly ILogger<CashFlowControlController> _logger;
        private readonly ITransactionBusiness _transactionBusiness;

        public CashFlowControlController(ILogger<CashFlowControlController> logger
        ,ITransactionBusiness transactionBusiness)
        {
            _logger = logger;
            _transactionBusiness = transactionBusiness;
        }

        [HttpPost("Credit")]
        public async Task<Transaction> Credit(Transaction transaction)
        {
            return await _transactionBusiness.Credit(transaction);
        }

        [HttpPost("Debit")]
        public async Task<Transaction> Debit(Transaction transaction)
        {
            return await _transactionBusiness.Debit(transaction);
        }

        [HttpGet("SumByDay")]
        public async Task<Transaction> SumByDay(long day)
        {
            return await _transactionBusiness.GetSumAmoutByDay(day);
        }
    }
}