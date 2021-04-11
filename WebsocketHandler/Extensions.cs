using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WebsocketHandler
{
    public static class Extensions
    {
        public static IServiceCollection AddWebSocketHandler(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();

            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(Handler))
                {
                    services.AddSingleton(type);
                }
            }

            return services;
        }

        public static IApplicationBuilder MapWebSocketHandler(this IApplicationBuilder app, PathString path, Handler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<Middleware>(handler));
        }
    }
}