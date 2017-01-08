using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RouteDelivery.Data;
using RouteDelivery.Data.Implementations;
using RouteDelivery.OptimizationEngine;
using Hangfire;
using Autofac;
using Autofac.Extensions.DependencyInjection;   
namespace RouteDelivery.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //string sConnectionString = Configuration["Data:Hangfire:ConnectionString"];
            //services.AddHangfire(x => x.UseSqlServerStorage(sConnectionString));
            services.AddHangfire(configuration => configuration.UseRedisStorage("127.0.0.1:6379"));
            
            services.AddDbContext<RouteDeliveryDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            // Add application services.
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IOptimizationEngine, OptimizationEngine.OptimizationEngine>();

            // Create the Autofac container builder.
            var builder = new ContainerBuilder();

            // Add any Autofac modules or registrations.
            builder.RegisterModule(new AutofacModule());

            // Populate the services.
            builder.Populate(services);

            // Build the container.
            this.ApplicationContainer = builder.Build();

            // Create and return the service provider.
            return new AutofacServiceProvider(this.ApplicationContainer);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
            // Map the Dashboard to the root URL
            //app.UseHangfireDashboard("");
            // Map to the '/dashboard' URL
            //app.UseHangfireDashboard("/dashboard");
            //default, Hangfire maps the dashboard to '/hangfire' URL
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}
