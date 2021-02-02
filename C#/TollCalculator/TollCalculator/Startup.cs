using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TollFeeCalculator.Logic;
using TollFeeCalculator.Models;
using TollFeeCalculator.Services;

namespace TollFeeCalculator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TollPassDatabaseSettings>(
            Configuration.GetSection(nameof(TollPassDatabaseSettings)));
            services.AddSingleton<ITollPassDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TollPassDatabaseSettings>>().Value);
            services.AddControllersWithViews();
            services.AddSingleton<ITollPassService, TollPassService>();
            services.AddTransient<ITollCalculator, TollCalculator>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("overview", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "TollFeeCalculator API",
                    Description = "Endpoints to manage Toll Fees",
                    Version = "1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Calculator/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/docs/{documentName}/";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/docs/overview/", "Overview");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Calculator}/{action=Index}/{id?}");
            });
        }
    }
}
