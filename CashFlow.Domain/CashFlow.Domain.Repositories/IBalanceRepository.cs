using System.Threading.Tasks;
using System;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Repositories;

public interface IBalanceRepository
{
    Task<IEnumerable<Balance>> GetByTime(int month, int day, int year);
}