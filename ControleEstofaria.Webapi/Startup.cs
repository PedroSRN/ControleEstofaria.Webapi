﻿using ControleEstofaria.Webapi.Config;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstofaria.Webapi
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
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(Startup));

            services.ConfigurarInjecaoDependencia();

            services.ConfigurarAutenticacao();

            services.ConfigurarFiltros();

            services.ConfigurarSwagger();

            services.ConfigurarJwt();

            services.AddCors(options =>
            {
                options.AddPolicy("Desenvolvimento",
                    services =>
                    services.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()

                    );

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleEstofaria.Webapi v1"));
            }



            app.UseCors("Desenvolvimento");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}