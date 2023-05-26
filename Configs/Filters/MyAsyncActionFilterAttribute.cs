using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace logTesting.Configs.Filters
{
    public class MyAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _callerName;

        public MyAsyncActionFilterAttribute(string callerName)
        {
            _callerName = callerName;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($" {_callerName}Async filter before executing action");
            await next();
            Console.WriteLine($" {_callerName}Async filter after executing action");

        }
    }
}