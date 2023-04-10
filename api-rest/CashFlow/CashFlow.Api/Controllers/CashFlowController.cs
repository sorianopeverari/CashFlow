using Microsoft.AspNetCore.Mvc;
using CashFlow.Domain.Models;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CashFlowControlController : ControllerBase
    {   private readonly ILogger<CashFlowControlController> _logger;

        public CashFlowControlController(ILogger<CashFlowControlController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Credit")]
        public Transaction Credit(double amount)
        {
            return new Transaction();
        }

        [HttpPost("Debit")]
        public Transaction Debit(double amount)
        {
            return new Transaction();
        }

        [HttpGet("Balance")]
        public Balance Get(long begin, long end)
        {
            return new Balance();
        }
    }
}