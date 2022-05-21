using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FoodTruckJunkie.ApiServer
{
    public class ErrorHandlingActionFilter : ExceptionFilterAttribute
    {
        private ILogger _logger;

        public ErrorHandlingActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.Error(context.Exception, context.Exception.ToString());

            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private static void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            
        }
    }
}