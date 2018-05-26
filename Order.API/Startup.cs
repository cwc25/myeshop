using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.API.IntegrationEvents;
using Order.API.IntegrationEvents.Events;
using Order.API.IntegrationEvents.EventsHandler;
using Order.API.Repository;

namespace Order.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MongoSetting>(options => Configuration.GetSection("MongoSetting").Bind(options));
            services.AddOptions();
            var temp = services.BuildServiceProvider().GetRequiredService<IOptions<MongoSetting>>();



            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
          
            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
				var rabbitService = Configuration.GetValue<string>("rabbitservice");
                
				return new EventBusRabbitMQ.EventBusRabbitMQ(iLifetimeScope, rabbitService);
            });

			//services.AddSingleton<IOrderQueries, OrderQueries>();
			//string connect = Configuration.GetValue<string>("sqlconnection");

			services.AddSingleton<IOrderQueries>(sp => 
			{
				return new OrderQueries(Configuration.GetValue<string>("sqlconnection"));
			});

            services.AddTransient<IOrderIntegrationEventService, OrderIntegrationEventService>();
            //services.AddTransient<OrderStartedIntegrationEventHandler>();
            services.AddTransient<UserCheckoutAcceptedIntegrationEventHandler>();
           
            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, UserCheckoutAcceptedIntegrationEventHandler>();
        }
    }
}
