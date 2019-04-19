using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayTaskList.Exceptions
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException)
            {
                var result = new ObjectResult(((ApiException)context.Exception).ErrorMessages);
                result.StatusCode = ((ApiException)context.Exception).StatusCode;
                context.Result = result;
            }
            else
            {
                var result = new ObjectResult("An error occured");
                result.StatusCode = 500;
                context.Result = result;
            }
            base.OnException(context);
        }
    }
}
