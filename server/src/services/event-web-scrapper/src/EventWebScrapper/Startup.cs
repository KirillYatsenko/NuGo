using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using EventWebScrapper.Services;
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

            registerRepositories(services);
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

        private void registerRepositories(IServiceCollection services)
        {
            services.AddTransient<IEventDateRepository, EventDateRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
        }

        private void registerServices(IServiceCollection services)
        {
            services.AddTransient<IKinoAfishaScrapper, KinoAfishaScrapper>();
            services.AddTransient<IKinoAfishaScrapperService, KinoAfishaScrapperService>();
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
