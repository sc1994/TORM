using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Monito.Models;
using Newtonsoft.Json;
using ORM;
using StackExchange.Redis;
using System;

namespace Monito
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var config = "118.24.27.231:6379";
            var conn = ConnectionMultiplexer.Connect(config);

            // 配置
            TORM.Options(options =>
                         {
                             options.DbConfig.Add("Log", "server=118.24.27.231;database=Log;uid=root;pwd=sun940622;");
                         });

            TORM.AutoTable<SqlLog>();
            // 在消费redis
            var sub = conn.GetSubscriber();
            sub.Subscribe("LogSql",
            (channel, message) =>
            {
                try
                {
                    _logger.LogInformation(message);
                    var info = JsonConvert.DeserializeObject<SqlLog>(message);
                    TORM.Insert(info);
                }
                catch (Exception e)
                {
                    _logger.LogInformation("ERROR：", e.ToString());
                }
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Dashboard/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
