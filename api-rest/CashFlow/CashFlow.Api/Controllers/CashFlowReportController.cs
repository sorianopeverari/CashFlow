using Microsoft.AspNetCore.Mvc;
using CashFlow.Domain.Models;
using CashFlow.Domain.Business;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<Balance> GetAmountSumByRange(long begin, long end)
        {
            return await _balanceBusiness.GetAmountSumByRange(begin, end);
        }

        [HttpPost("Balance")]
        public async Task<ActionResult> Post(long day, double amountSum)
        {
            await _balanceBusiness.Create(day, amountSum);
            return Ok();
        }
    }
}