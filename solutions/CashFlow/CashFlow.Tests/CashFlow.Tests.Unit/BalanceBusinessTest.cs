using Xunit;
using Moq;
using CashFlow.Domain.Business;
using CashFlow.Domain.Models;
using CashFlow.Domain.Models.Utils;
using CashFlow.Domain.Repositories;
using System.Threading.Tasks;
using System;
using System.Linq;
using CashFlow.Domain.Models.Enums;
using System.Collections.Generic;

namespace CashFlow.Tests.Unit;

public class BalanceBusinessTest
{
    [Theory]
    [InlineData(10, 10)]
    [InlineData(10.2, 10.2)]
    [InlineData(12012.2, 12012.2)]
    public async void GetAmountSumByRange_GetAmout_ReturnSameAmout(double input, double expected)
    {
        BalanceBusiness balanceBusiness = this.SetupDefaultBalanceBusiness(input);

        Balance actualBalance = await balanceBusiness
                            .GetAmountSumByRange(DateUtil.ToTimestampOnlyDate(DateTime.UtcNow),
                                                 DateUtil.ToTimestampOnlyDate(DateTime.UtcNow),
                                                 BalanceType.Undefined);
        
        double actual = ((List<Transaction>)actualBalance.Transactions).FirstOrDefault().Amount;

        Assert.Equal(expected, actual);
    }

    private BalanceBusiness SetupDefaultBalanceBusiness(double balance = 0)
    {
        Mock<IBalanceRepository> balanceRepository = new();
        balanceRepository.Setup(x => x.Create(It.IsAny<long>(),
                                              It.IsAny<double>(),
                                              It.IsAny<BalanceType>()))
                         .Returns(Task.CompletedTask);

        IEnumerable<Transaction> transactions = new List<Transaction>()
        {
            new Transaction() 
            { 
                EffectiveDate = DateUtil.ToTimestamp(DateTime.UtcNow.Date),
                Amount = balance  
            }
        };

        balanceRepository.Setup(x => x.GetAmountSumByRange(It.IsAny<long>(),
                                                           It.IsAny<long>(),
                                                           It.IsAny<BalanceType>()))
                         .Returns(Task.FromResult(new Balance()
                         {  
                            Begin = DateUtil.ToTimestamp(DateTime.UtcNow.Date),
                            End = DateUtil.ToTimestamp(DateTime.UtcNow.Date),
                            Transactions = transactions
                         }));
        
        return new BalanceBusiness(balanceRepository.Object);
    }
}