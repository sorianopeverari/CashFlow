using System;

namespace CashFlow.Domain.Repositories.Exceptions
{
    public class ViolationRepositoryException : RepositoryException
    {
        public ViolationRepositoryException()
        {
        }

        public ViolationRepositoryException(string message)
            : base(message)
        {
        }

        public ViolationRepositoryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }    
}
