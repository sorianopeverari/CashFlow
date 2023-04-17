using CashFlow.Domain.Models.Utils;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repositories;

namespace CashFlow.Domain.Business
{
    public class TransactionBusiness : AbstracBusiness, ITransactionBusiness
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionBusiness(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> Credit(Transaction transaction)
        {
            this.BindAndValidationToCreate(transaction);
            return await _transactionRepository.Create(transaction);
        }

        public async Task<Transaction> Debit(Transaction transaction)
        {
            this.BindAndValidationToCreate(transaction);
            transaction.Amount = transaction.Amount * (-1);
            long balanceDate = DateUtil.ToTimestamp(DateUtil.ToDateTime(transaction.EffectiveDate).Date);
            return await _transactionRepository.CreateIfBalancePositive(transaction, balanceDate);
        }
        public async Task<double> GetSumAmout(long effectiveDate)
        {
            return await _transactionRepository.GetSumAmout(effectiveDate);
        }

        private void BindAndValidationToCreate(Transaction transaction)
        {
            if(transaction.Amount <= 0)
            {
                throw new Exception("Transaction amount can't lower or equals than zero");
            }
            transaction.EffectiveDate = DateUtil.ToTimestamp(DateTime.UtcNow);
        }
    }
}
