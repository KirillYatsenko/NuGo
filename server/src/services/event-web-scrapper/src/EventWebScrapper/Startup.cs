using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventWebScrapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            registerServices(services);
            registerDbContext(services);
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
        }

        private void registerServices(IServiceCollection services)
        {
            services.AddTransient<IEventScrapDataRepository, EventScrapDataRepository>();
            services.AddTransient<IKinoAfishaScrapper, KinoAfishaScrapper>();
        }

        private void registerDbContext(IServiceCollection services)
        {
            var sqlConnection = Configuration["ConnectionString"];

            if (string.IsNullOrWhiteSpace(sqlConnection))
            {
                throw new ConfigurationErrorsException("ConnectionString entry can not be found, please check appsettings");
            }

            services.AddDbContext<EventWebScrapperDbContext>(options => options.UseMySQL(sqlConnection));
        }
    }
}
