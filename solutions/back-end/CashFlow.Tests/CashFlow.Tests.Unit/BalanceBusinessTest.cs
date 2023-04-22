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
using CashFlow.Domain.Business.Providers;

namespace CashFlow.Tests.Unit;

public class BalanceBusinessTest
{
    [Theory]
    [InlineData(10, 10)]
    [InlineData(10.2, 10.2)]
    [InlineData(12012.2, 12012.2)]
    public async void GetAmountSumByRange_GetAmout_ReturnSameAmout(double input, double expected)
    {
        BalanceBusiness arrangeBalanceBusiness = this.SetupDefaultBalanceBusiness(input);

        Balance actBalance = await arrangeBalanceBusiness
                            .GetAmountSumByRange(DateUtil.ToTimestampOnlyDate(DateTime.UtcNow),
                                                 DateUtil.ToTimestampOnlyDate(DateTime.UtcNow),
                                                 BalanceType.Undefined);
        double actual = ((List<Transaction>)actBalance.Transactions).FirstOrDefault().Amount;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(10.2)]
    [InlineData(12012.2)]
    public void Create_CreatePositiveAmount_ReturnsTaskCompleted(double fakeAmount)
    {
        long mockTime = this.SetupDateTimeProvider().UtcNow();
        BalanceType mockBalanceType = BalanceType.Undefined;
        BalanceBusiness stubBalanceBusiness = this.SetupDefaultBalanceBusiness();
        
        Exception actual = Record.Exception(
            () => stubBalanceBusiness.Create(mockTime, fakeAmount, mockBalanceType).Start());
        
        Assert.IsNotType<Exception>(actual);
    }

    private IDateTimeProvider SetupDateTimeProvider()
    {
        Mock<IDateTimeProvider> dateTimeProvider = new();
        dateTimeProvider.Setup(x => x.UtcNow())
                        .Returns(DateUtil.ToTimestamp(DateTime.UtcNow));

        return dateTimeProvider.Object;
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