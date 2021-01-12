using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Attributes
{
        [AttributeUsage(validOn: AttributeTargets.Class)]
        public class ApiKeyAttribute : Attribute, IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.HttpContext.Request.Headers.TryGetValue("Api-Key", out var extractedApiKey))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "Rejected request"
                    };
                    return;
                }

                var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                var apiKey = appSettings.GetValue<string>("Api-Key");

                if (!apiKey.Equals(extractedApiKey))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "Invalid api key"
                    };
                    return;
                }

                await next();
            }
        }
    }

