using System;

namespace CashFlow.Domain.Repositories.Exceptions
{
    public class UnavailableRepositoryException : RepositoryException
    {
        public UnavailableRepositoryException()
        {
        }

        public UnavailableRepositoryException(string message)
            : base(message)
        {
        }

        public UnavailableRepositoryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }    
}
