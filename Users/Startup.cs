using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using HttpClientFactory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using Polly;
using Polly.Extensions.Http;

namespace Users
{
    public class Startup
    {
      
        private readonly NLog.Logger _logger = new NLog.LogFactory().GetCurrentClassLogger();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpClient("Hierarchy", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44348/");
                // add user-agent if required
                c.DefaultRequestHeaders.Add("User-Agent", "Users");
            })
            .AddPolicyHandler(HttpClientFactoryPollyWrapper.GetHierarchyServiceRetryPolicy());
           // .AddPolicyHandler(HttpClientFactoryPollyWrapper.GetCircuitBreakerPolicy());
            //.Fallback(() => DoFallbackAction());
        }


        // The below policy will try to reconnect for 3 times every 2 seconds. This Policy takes care of any responses with 5xx status code and 408(request timeout) status code
        //Still need to add Jitter stratagy for WaitAndRetry
        //.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
        //onRetry: (outcome, timespan, retryCount, context) =>
        //{
        //    _logger.Log(NLog.LogLevel.Info, $"Delaying for: {timespan.TotalMilliseconds}ms, then making retry {retryCount} ");
        //}
        //))

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();
            app.AddNLogWeb();
        }
    }
}
