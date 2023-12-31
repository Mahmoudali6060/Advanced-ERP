﻿using Infrastructure.Contracts;
using Infrastructure.ExceptionHandling.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExceptionHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (Exception ex)
            {

                _logger.LogError($"Exception: {ex}");

                //_logger.LogError($"Exception: {ex} Message: {ex.Message} Source:{ex.Source} StackTrace:{ex.StackTrace} InnerException:{ex.InnerException}");
                await HandleExceptionAsync(httpContext, ex);

            }


        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = exception.Message;
            ////To-Do
            if (exception.InnerException != null
                && exception.InnerException.Message != null
                && exception.InnerException.Message.Contains("DELETE"))
            {
                message = "Errors.CannotDeleteThisRecordDueToRelatedToAnotherData";
            }
            if (exception.InnerException != null
               && exception.InnerException.Message != null
               && exception.InnerException.Message.Contains("UNIQUE"))
            {
                message = "Errors.DuplicatedFullName";
            }

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
