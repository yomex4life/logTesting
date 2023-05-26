using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace logTesting.Configs.Filters
{
    public class MyActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly string _callerName;

        public MyActionFilterAttribute(string CallerName)
        {
            _callerName = CallerName;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($" {_callerName} - Action is executed: onActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($" {_callerName} Action is executing: OnActionExecuting");
        }
    }
}