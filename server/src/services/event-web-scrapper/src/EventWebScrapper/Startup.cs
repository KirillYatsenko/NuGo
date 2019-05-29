using System;
using System.Configuration;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Abstracts;
using EventBus.Implementations;
using EventWebScrapper.EventHandlers;
using EventWebScrapper.IntegrationEvents;
using EventWebScrapper.Repositories;
using EventWebScrapper.Scrappers;
using EventWebScrapper.Scrappers.KinoAfishaScrappers;
using EventWebScrapper.Scrappers.KoncertUAScrappers;
using EventWebScrapper.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScrapySharp.Network;

namespace EventWebScrapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = 
                                        Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            registerRepositories(services);
            registerDbContext(services);
            registerServices(services);
            registerEventBus(services);

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
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

            ConfigureEventBus(app);
        }

        private void registerRepositories(IServiceCollection services)
        {
            services.AddSingleton<IEventsScheduleRepository, EventsScheduleRepository>();
            services.AddSingleton<IEventsRepository, EventsRepository>();
            services.AddSingleton<IEventsCategoryRepository, EventsCategoryRepository>();
        }

        private void registerServices(IServiceCollection services)
        {
            services.AddTransient<IKinoAfishaScrapper, KinoAfishaScrapper>();
            services.AddTransient<IKinoAfishaService, KinoAfishaService>();
            services.AddTransient<IKoncertUAService, KoncertUAService>();
            services.AddTransient<IKoncertUAScrapper, KoncertUAScrapper>();
            services.AddTransient<IFileBrowserService, FileBrowserService>();
            services.AddTransient<IFileDownloaderService, FileDownloaderService>();
            services.AddTransient<IEventImageScrapper, EventImageScrapper>();
            services.AddTransient<ISingletSessionScrapper, SingletSessionScrapper>();
            services.AddTransient<IMultipleSessionsScrapper, MultipleSessionsScrapper>();
            services.AddTransient<KoncertUaEventHandler>();
            services.AddTransient<ScrapKinoafishaEventHandler>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IEventsCategoryService, EventsCategoryService>(); 
            services.AddTransient<ISchedulesService, SchedulesService>(); 
            services.AddTransient<IEventScheduler, EventScheduler>();

            services.AddTransient<ScrapingBrowser>(serviceProvider =>
            {
                var browser = new ScrapingBrowser();
                browser.AllowAutoRedirect = true;
                browser.AllowMetaRedirect = true;
                browser.Encoding = Encoding.UTF8;

                return browser;
            });
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

        private void registerEventBus(IServiceCollection services)
        {
            var rabbitmqConfig = new RabbitmqConfiguration(); 
            Configuration.Bind("Rabbitmq", rabbitmqConfig);
            services.AddSingleton(rabbitmqConfig);

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                var iLifetimeScope = serviceProvider.GetRequiredService<ILifetimeScope>();

                return new EventBusRabbitMQ(iLifetimeScope, rabbitmqConfig);
            });
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<KoncertUaEventHandler, ScrapIntegrationEvent>(Configuration["ServiceName"], "KoncertUA");
            eventBus.Subscribe<ScrapKinoafishaEventHandler, ScrapIntegrationEvent>(Configuration["ServiceName"], "KinoAfisha");
        }

    }
}
