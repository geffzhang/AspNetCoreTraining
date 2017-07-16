using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWebsite.Middlewares;
using System.Threading;

namespace Middleware
{
    public class Startup
    {
        private static ILogger _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("You ended in the conditional test branch");
            });
        }

        private static void TvdDelegate(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Branched to Trivadis!");
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory , IApplicationLifetime appLifetime)
        {
            _logger = loggerFactory.AddConsole().CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(next => async context =>
            {
                await context.Response.WriteAsync("Visited USE.");
                await next.Invoke(context);
            });

            app.UseMiddleware<FirstMiddleware>();
            app.UseMiddleware<SecondMiddleware>();
            app.UseMiddleware<ThirdMiddleware>();

            app.UseOwnMiddleware();

            app.Map("/TvdDelegate", TvdDelegate);

            app.MapWhen(context => context.Request.Path.Value.Contains("test"), HandleBranch);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            appLifetime.ApplicationStarted.Register(() =>
            {
                _logger.LogDebug("ApplicationLifetime - Started");
            });
            appLifetime.ApplicationStopping.Register(() =>
            {
                _logger.LogDebug("ApplicationLifetime - Stopping");
            });
            appLifetime.ApplicationStopped.Register(() =>
            {
                Thread.Sleep(10 * 1000);
                _logger.LogDebug("ApplicationLifetime - Stopped");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            // For trigger stop application
            var thread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(10 * 1000);
                appLifetime.StopApplication();
            }));
            thread.Start();
        }
    }
}
