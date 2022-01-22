using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RubiconeX_Serv.AutomapperProfile;
using RubiconeX_Serv.BusinessLogic.AutoMapperProfile;
using RubiconeX_Serv.BusinessLogic.Core.Interfaces;
using RubiconeX_Serv.BusinessLogic.Services;
using RubiconeX_Serv.DataAccsess.Core.Interfaces.Context;
using RubiconeX_Serv.DataAccsess.DbContext;

namespace RubiconeX_Serv
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
            services.AddControllers();
            services.AddAutoMapper(typeof(BusinessLogicProfile), typeof(MicroserviceProfile));
            services.AddDbContext<IRubiconeX_ServContext, RubiconeXContext>(o => o.UseSqlite("Data Source=userdata.db; Foreign Keys=True"));
            services.AddScoped<IUseerServece, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseCors(p => p.AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthorization();
            using var scope = app.ApplicationServices.CreateScope();

            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            var dbContext = scope.ServiceProvider.GetRequiredService<RubiconeXContext>();
            dbContext.Database.Migrate();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
