using Infrastructure.Contracts;
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
    public class DBExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        public DBExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
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

            catch (DbException ex)
            {

                switch (ex.ErrorCode)
                {
                    case 547: // Foreign Key violation
                        throw new InvalidOperationException("Some helpful description", ex);
                }
                await _next(httpContext);
            }


        }

    }
}
