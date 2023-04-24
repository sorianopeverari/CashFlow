using Microsoft.AspNetCore.Mvc;
using CashFlow.Domain.Models;
using CashFlow.Domain.Business;
using Microsoft.FeatureManagement.Mvc;
using CashFlow.Api.Confgis;
using CashFlow.Domain.Models.Enums;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [FeatureGate(FeatureFlags.Report)]
    public class CashFlowReportController : ControllerBase
    {   
        private readonly ILogger<CashFlowReportController> _logger;
        private readonly IBalanceBusiness _balanceBusiness;

        public CashFlowReportController(ILogger<CashFlowReportController> logger
        ,IBalanceBusiness balanceBusiness)
        {
            _balanceBusiness = balanceBusiness;
            _logger = logger;
        }

        [HttpGet("Balance")]
        public async Task<Balance> GetAmountSumByRange([FromQuery]long begin, [FromQuery]long end)
        {
            BalanceType balanceType = BalanceType.SumByDay;
            return await _balanceBusiness.GetAmountSumByRange(begin, end, balanceType);
        }

        [HttpPost("Balance")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task Create(Transaction balance)
        {
            BalanceType balanceType = BalanceType.SumByDay;
            await _balanceBusiness.Create(balance.EffectiveDate, balance.Amount, balanceType);
        }
    }
}