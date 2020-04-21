using UserManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserManagement.Provider
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is UserException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as UserException;
                context.Exception = null;
                var apiError = new ResponseModel
                {
                    Message = ex.Message,
                    // Data = ex.InnerException
                };
                context.Result = new JsonResult(apiError);
                context.HttpContext.Response.StatusCode = 500;
            }
            else
            {
                var apiError = new ResponseModel
                {
                    Message = context.Exception.GetBaseException().Message,
                    // Data = context.Exception?.InnerException
                };
                context.Result = new JsonResult(apiError);
                context.HttpContext.Response.StatusCode = 500;
            }
        }
    }
}
