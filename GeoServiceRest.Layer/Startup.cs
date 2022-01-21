using GeoService.Core.Repository;
using GeoService.Core.Service;
using GeoService.Core.UnitOfWorks;
using GeoService.DataEF;
using GeoService.DataEF.Repositories;
using GeoService.DataEF.UnitOfWorks;
using GeoService.Service.Services;
using GeoServiceRest.Layer.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<NotFoundFilterContinent>();
            services.AddScoped<NotFoundFilterCountry>();
            services.AddScoped<NotFoundFilterCity>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IContinentService, ContinentService>();
            services.AddScoped<IUnitOfWorks, UnitOfWork>();


            services.AddDbContext<AppDBContext>(option => {
                option.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o => {
                    o.MigrationsAssembly("GeoService.DataEF");
                });
            });
            //services.AddDbContext<AppDBContext>(option => {
            //    option.UseSqlServer(Configuration.GetConnectionString("SqlConStr"));
            //});
            services.AddControllers(option => {
                option.Filters.Add(new ValidationFilter());
            });

            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });

            //services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeoServiceRest.Layer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoServiceRest.Layer v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
