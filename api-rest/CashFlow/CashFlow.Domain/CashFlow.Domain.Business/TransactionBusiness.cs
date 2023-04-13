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
            this.BindCommon(transaction);
            return await _transactionRepository.Credit(transaction);
        }

        public async Task<Transaction> Debit(Transaction transaction)
        {
            this.BindCommon(transaction);
            transaction.Amount = transaction.Amount * (-1);
            return await _transactionRepository.Debit(transaction);
        }

        public async Task<Transaction> GetSumAmoutByDay(long day)
        {
            return await _transactionRepository.GetSumAmoutByDay(day);
        }
        

        private void BindCommon(Transaction transaction)
        {
            if(transaction.Amount <= 0)
            {
                throw new Exception("Transaction amount can't lower or equals than zero");
            }
            transaction.EffectiveDate = DateUtil.ToTimestamp(DateTime.Now);
        }
    }
}
