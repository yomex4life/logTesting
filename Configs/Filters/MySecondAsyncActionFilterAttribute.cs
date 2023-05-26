using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace logTesting.Configs.Filters
{
    public class MySecondAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _callerName;

        public MySecondAsyncActionFilterAttribute(string callerName)
        {
            _callerName = callerName;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($" {_callerName} Second Async filter before executing action");
            await next();
            Console.WriteLine($" {_callerName} Second Async filter after executing action");
        }
    }
}