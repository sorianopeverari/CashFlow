using Xunit;
using Moq;
using CashFlow.Domain.Business;
using CashFlow.Domain.Models;
using CashFlow.Domain.Models.Utils;
using CashFlow.Domain.Repositories;
using System.Threading.Tasks;
using CashFlow.Domain.Business.Providers;
using System;
using CashFlow.Domain.Business.Exceptions;

namespace CashFlow.Tests.Unit;

public class TransactionBusinessTest
{
    [Fact]
    public void Credit_AmountEqZero_ThrowValidationException()
    {
        TransactionBusiness transactionBusiness = this.SetupDefaultTransactionBusiness();
        Transaction inputTransaction = new Transaction() { Amount = 0 };

        Assert.ThrowsAsync<ValidationException>(() => 
            transactionBusiness.Credit(inputTransaction));
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(10.2, 10.2)]
    [InlineData(12012.2, 12012.2)]
    public async void Credit_PositiveAmount_ReturnSameAmount(double input, double expected)
    {
        TransactionBusiness transactionBusiness = this.SetupDefaultTransactionBusiness();
        Transaction inputTransaction = new Transaction() { Amount = input };

        Transaction actualTransaction = await transactionBusiness.Credit(inputTransaction);
        double actual = actualTransaction.Amount;
        
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-10.2)]
    [InlineData(-12012.2)]
    public void Credit_NegativeAmount_ThrowValidationException(double input)
    {
        TransactionBusiness transactionBusiness = this.SetupDefaultTransactionBusiness();
        Transaction inputTransaction = new Transaction() { Amount = input };

        Assert.ThrowsAsync<ValidationException>(() => 
            transactionBusiness.Credit(inputTransaction));
    }

    [Fact]
    public void Debit_ZeroEqAmount_ThrowValidationException()
    {
        TransactionBusiness transactionBusiness = this.SetupDefaultTransactionBusiness();
        Transaction inputTransaction = new Transaction() { Amount = 0 };

        Assert.ThrowsAsync<ValidationException>(() => 
            transactionBusiness.Debit(inputTransaction));
    }

    [Theory]
    [InlineData(10, -10)]
    [InlineData(10.2, -10.2)]
    [InlineData(12012.2, -12012.2)]
    public async void Debit_PositiveAmount_ReturnSameNegativeAmount(double input, double expected)
    {
        TransactionBusiness transactionBusiness = this.SetupDefaultTransactionBusiness(input);
        Transaction inputTransaction = new Transaction() { Amount = input };

        Transaction actualTransaction = await transactionBusiness.Debit(inputTransaction);
        double actual = actualTransaction.Amount;
        
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-10.2)]
    [InlineData(-12012.2)]
    public void Debit_NegativeAmount_ThrowValidationException(double input)
    {
        TransactionBusiness transactionBusiness = this.SetupDefaultTransactionBusiness(input);
        Transaction inputTransaction = new Transaction() { Amount = input };

        Assert.ThrowsAsync<ValidationException>(() => 
            transactionBusiness.Debit(inputTransaction));
    }

    [Theory]
    [InlineData(10, 9.9)]
    [InlineData(10.2, 10.111)]
    [InlineData(12012.2, 12011.02)]
    public void Debit_DailyBalanceNotEnought_ThrowValidationException(double input, double balance)
    {
        TransactionBusiness transactionBusiness = this.SetupDefaultTransactionBusiness(balance);
        Transaction inputTransaction = new Transaction() { Amount = input };

        Assert.ThrowsAsync<ValidationException>(() => 
            transactionBusiness.Debit(inputTransaction));
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(10.2, 10.2)]
    [InlineData(12012.2, 12012.2)]
    [InlineData(-10, -10)]
    [InlineData(-10.2, -10.2)]
    [InlineData(-12012.2, -12012.2)]
    [InlineData(0, 0)]
    public async void GetSumAmout_GetBalance_ReturnSameBalance(double input, double expected)
    {
        TransactionBusiness transactionBusiness = this.SetupDefaultTransactionBusiness(input);

        double actual = await transactionBusiness
                            .GetSumAmout(DateUtil.ToTimestampOnlyDate(DateTime.UtcNow));
        
        Assert.Equal(expected, actual);
    }

    private TransactionBusiness SetupDefaultTransactionBusiness(double balance = 0)
    {
        Mock<IDateTimeProvider> dateTimeProvider = new();
        dateTimeProvider.Setup(x => x.UtcNow())
                        .Returns(DateUtil.ToTimestamp(DateTime.UtcNow));

        Mock<ITransactionRepository> transactionRepository = new();
        transactionRepository.Setup(x => x.Create(It.IsAny<Transaction>()))
            .Returns(Task.CompletedTask);
        transactionRepository.Setup(x => x.GetSumAmout(It.IsAny<long>()))
            .Returns(Task.FromResult(balance));
        
        return new TransactionBusiness(transactionRepository.Object, dateTimeProvider.Object);
    }
}