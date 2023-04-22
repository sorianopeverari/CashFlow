using System;

namespace CashFlow.Domain.Repositories.Exceptions
{
    public class UnexpectedRepositoryException : RepositoryException
    {
        public UnexpectedRepositoryException()
        {
        }

        public UnexpectedRepositoryException(string message)
            : base(message)
        {
        }

        public UnexpectedRepositoryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }    
}
