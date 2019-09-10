using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using DayTaskList.Configuration;
using DayTaskList.Exceptions;
using DayTaskListData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace DayTaskList
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
            services.AddDbContext<TaskListContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("TaskListConnection")));

            Action<IMapperConfigurationExpression> mapperExpression = (mc =>
            {
                mc.AddProfile(new AutoMapperApiService().GetMappingProfile());
            });

            services.AddAutoMapper(mapperExpression);


            services.AddMvc(
                 options => options.Filters.Add(new ApiExceptionFilterAttribute())
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            new ApiDependencyInjectionService().RegisterDependencies(services);

            //results in swagger spec being available at https://localhost:44324/swagger/OpenAPISpec/swagger.json
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("OpenAPISpec", new OpenApiInfo
                {
                    Title = "Task List API",
                    Version = "1"
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DayTaskList.xml"), true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/OpenAPISpec/swagger.json", "Task List API");
                options.RoutePrefix = "";
            });
            app.UseMvc();
        }
    }
}
