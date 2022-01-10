using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftwareMarket.Core;
using SoftwareMarket.Core.Services;

namespace SoftwareMarket.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddScoped<ConnectionConfig>();
            services.AddScoped<SoftwareTypeService>();
            services.AddScoped<BuyerTypeService>();
            services.AddScoped<DiscountService>();
            services.AddScoped<SoftwareService>();
            services.AddScoped<BuyerService>();
            services.AddScoped<SaleService>();
        }
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftwareMarket v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            
            app.UseHttpsRedirection();
            app.UseRouting();
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}