using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionManagement.Shared;

namespace TransactionManagement.Server.Utils
{
    public class ExceprionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceprionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (TransactionManagementException ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    //log error
                    throw;
                }

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 400;

                await httpContext.Response.WriteAsync(ex.Message);

                return;
            }
            catch(Exception)
            {
                if (httpContext.Response.HasStarted)
                {
                    //log error
                    throw;
                }

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 500;

                return;
            }
        }
    }
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceprionHandlingMiddleware>();
        }
    }
}
