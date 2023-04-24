using CashFlow.Domain.Business.Exceptions;
using CashFlow.Domain.Repositories.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = CashFlow.Domain.Business.Exceptions.ValidationException;

namespace CashFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int? status = 500;
            
            if(context.Exception is ValidationException)
            {
                status = 400;
            } 
            else if(context.Exception is BusinessRuleException) 
            {
                status = 403;

            } 
            else if(context.Exception is UnavailableRepositoryException) 
            {
                status = 503;
            }

            context.Result = new ContentResult
            {
                StatusCode = status,
                Content = context.Exception.ToString()
            };
        }
    }
}