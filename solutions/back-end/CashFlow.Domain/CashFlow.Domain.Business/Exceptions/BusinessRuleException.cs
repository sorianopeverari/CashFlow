using System;

namespace CashFlow.Domain.Business.Exceptions
{
    public class BusinessRuleException : BusinessException
    {
        public BusinessRuleException()
        {
        }

        public BusinessRuleException(string message)
            : base(message)
        {
        }

        public BusinessRuleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }    
}