using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Poc.FilterMiddleware.Api.Configurations;
using Poc.FilterMiddleware.Api.Configurations.Filters;

namespace Poc.FilterMiddleware.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .RegisterFeatures()
                .ConfigureSettingsSwagger(Configuration)
                .RegisterSwagger(Configuration)
                .RegisterValidations()
                .AddControllers(o => 
                {
                    // adicionando o filtro em contexto global
                    //o.Filters.Add<ModelStateValidationFilterAttribute>();
                    //o.Filters.Add<ExceptionServiceFilterAttribute>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptionsMonitor<List<OpenApiInfo>> optionsMonitor)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .UseSwagger(optionsMonitor);
        }
    }
}
