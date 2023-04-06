using Microsoft.AspNetCore.Mvc;
using CashFlow.Domain.Models;

namespace CashFlow.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CashFlowControlController : ControllerBase
{   private readonly ILogger<CashFlowControlController> _logger;

    public CashFlowControlController(ILogger<CashFlowControlController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{month}/{day}/{year}")]
    public IEnumerable<Balance> Get(int month, int day, int year)
    {
        return new List<Balance>();
    }

    [HttpPost]
    public Transaction Post(double ammount)
    {
        return new Transaction(){ Date = DateTime.Now, Amount = ammount };
    }
}
