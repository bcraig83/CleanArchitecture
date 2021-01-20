using Audit.Core;
using Audit.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApi;

namespace WebApi
{
    public static class AuditConfiguration
    {
        private const string CorrelationIdField = "CorrelationId";

        /// <summary>
        /// Add the global audit filter to the MVC pipeline
        /// </summary>
        public static MvcOptions AddAudit(this MvcOptions mvcOptions)
        {
            // Configure the global Action Filter
            mvcOptions.AddAuditFilter(a => a
                    .LogAllActions()
                    .WithEventType("MVC:{verb}:{controller}:{action}")
                    .IncludeModelState()
                    .IncludeRequestBody()
                    .IncludeResponseBody());
            return mvcOptions;
        }

        /// <summary>
        /// Global Audit configuration
        /// </summary>
        public static IServiceCollection ConfigureAudit(this IServiceCollection serviceCollection)
        {
            // TODO: Configure the audit data provider and options. For more info see https://github.com/thepirat000/Audit.NET#data-providers.
            Configuration.Setup()
                .UseFileLogProvider(_ => _
                    .DirectoryBuilder(_ => $@"C:\Temp\{DateTime.Now:yyyy-MM-dd}")
                    .FilenameBuilder(auditEvent => $"{auditEvent.Environment.UserName}_{auditEvent.StartDate:yyyyMMddHHmmssffff}.json"))
                .WithCreationPolicy(EventCreationPolicy.InsertOnEnd);

            return serviceCollection;
        }

        public static void UseAuditMiddleware(this IApplicationBuilder app)
        {
            // Configure the Middleware
            app.UseAuditMiddleware(_ => _
                .FilterByRequest(r => !r.Path.Value.EndsWith("favicon.ico"))
                .IncludeHeaders()
                .IncludeRequestBody()
                .IncludeResponseBody()
                .WithEventType("HTTP:{verb}:{url}"));
        }
    }
}