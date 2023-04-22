using CashFlow.Domain.Models.Utils;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Business.Exceptions;
using System.Transactions;
using Transaction = CashFlow.Domain.Models.Transaction;
using CashFlow.Domain.Business.Providers;

namespace CashFlow.Domain.Business
{
    public class TransactionBusiness : AbstracBusiness, ITransactionBusiness
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TransactionBusiness(ITransactionRepository transactionRepository
                                   ,IDateTimeProvider dateTimeProvider)
        {
            _transactionRepository = transactionRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Transaction> Credit(Transaction transaction)
        {
            this.ValidationPositiveAmount(transaction);
            Transaction credit = this.Bind(transaction);
            await _transactionRepository.Create(credit);
            return credit;
        }

        public async Task<Transaction> Debit(Transaction transaction)
        {
            this.ValidationPositiveAmount(transaction);
            Transaction debit = this.Bind(transaction);

            using(TransactionScope ts = new TransactionScope())
            {
                this.ValidationEnoughDailyBalance(debit);
                debit.Amount = debit.Amount * (-1);
                await _transactionRepository.Create(debit);
                ts.Complete();
            }

            return debit;
        }
        public async Task<double> GetSumAmout(long effectiveDate)
        {
            return await _transactionRepository.GetSumAmout(effectiveDate);
        }

        private async void ValidationEnoughDailyBalance(Transaction transaction)
        {
            long balanceDate = DateUtil.ToTimestampOnlyDate(transaction.EffectiveDate);
            double balanceAmount = await _transactionRepository.GetSumAmout(balanceDate);
            
            if((balanceAmount + transaction.Amount) < 0)
            {
                throw new BusinessRuleException("Daily balance isn't enought");
            }
        }

        private void ValidationPositiveAmount(Transaction transaction)
        {
            if(transaction.Amount <= 0)
            {
                throw new ValidationException("Negative amount value");
            }
        }

        private Transaction Bind(Transaction transaction)
        {
            return new Transaction()
            {
                Amount = transaction.Amount,
                EffectiveDate = _dateTimeProvider.UtcNow()
            };
        } 
    }
}