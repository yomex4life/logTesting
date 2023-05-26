using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace logTesting.Configs.Filters
{
    public class MyFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Action is executing: OnActionExecuting");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"Action is executed: onActionExecuted");
        }


    }
}