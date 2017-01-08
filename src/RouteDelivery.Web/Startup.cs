using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RouteDelivery.Data;
using Hangfire;
using RouteDelivery.Data.Implementations;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

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

            

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //string sConnectionString = Configuration["Data:Hangfire:ConnectionString"];
            //services.AddHangfire(x => x.UseSqlServerStorage(sConnectionString));
            services.AddHangfire(configuration => configuration.UseRedisStorage("127.0.0.1:6379"));
            
            services.AddDbContext<RouteDeliveryDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            // Add application services.
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<OptimizationEngine.IOptimizationEngine, OptimizationEngine.OptimizationEngine>();
   
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
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("zh"),
                new CultureInfo("es")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
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
        }
    }
}
