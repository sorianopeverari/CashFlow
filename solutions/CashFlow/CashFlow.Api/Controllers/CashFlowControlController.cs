using Microsoft.AspNetCore.Mvc;
using CashFlow.Domain.Models;
using CashFlow.Domain.Business;
using Microsoft.FeatureManagement.Mvc;
using CashFlow.Api.Confgis;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    [FeatureGate(FeatureFlags.Control)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Transaction> Credit(Transaction transaction)
        {
            return await _transactionBusiness.Credit(transaction);
        }

        [HttpPost("Debit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Transaction> Debit(Transaction transaction)
        {
            return await _transactionBusiness.Debit(transaction);
        }

        [HttpGet("SumAmmout")]
        public async Task<double> SumAmmout(long balanceDate)
        {
            return await _transactionBusiness.GetSumAmout(balanceDate);
        }
    }
}
