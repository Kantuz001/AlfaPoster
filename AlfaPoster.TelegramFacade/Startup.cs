using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlfaPoster.TelegramFacade.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlfaPoster.TelegramFacade
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.Configure<Config>(Configuration);
            var settings = Configuration.Get<Config>();
            
            Bot.Init(settings.TelegramBotToken, settings.TelegramBotName);
            if (settings.Ngrok)
            {
                var ngrockUrl = Ngrok.GetTunnelUrl();
                Bot.Api.SetWebhookAsync(ngrockUrl + "/TelegramBot/Message").Wait();
            }
            else
                Bot.Api.SetWebhookAsync(settings.HostUrl + "/TelegramBot/Message").Wait();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(LogLevel.Information);
                loggerFactory.AddDebug();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultRoute",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}