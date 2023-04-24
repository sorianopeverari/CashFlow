using System;

namespace CashFlow.Domain.Business.Exceptions
{
    public class ValidationException : BusinessException
    {
        public ValidationException()
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }    
}
