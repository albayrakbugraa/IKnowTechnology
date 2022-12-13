using FluentValidation.AspNetCore;
using IKnowTechnology.BLL.AutoMapper;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Services.TaskListService;
using IKnowTechnology.BLL.Services.UserService;
using IKnowTechnology.CORE.Entities;
using IKnowTechnology.CORE.IRepositories;
using IKnowTechnology.DAL.DbContext;
using IKnowTechnology.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
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

namespace IKnowTechnology.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IKnowTechnologyCase.API", Version = "v1" });

            });
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnString"));
            });
            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = false;
                config.User.RequireUniqueEmail = false;  // unique
                config.User.AllowedUserNameCharacters = "abcçdefghiýjklmnoöpqrsþtuüvwxyzABCÇDEFGHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789-._@+";
            }).AddEntityFrameworkStores<AppDbContext>()
           .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider)
           .AddEntityFrameworkStores<AppDbContext>();

            services.AddCors(options => options.AddPolicy("AllowOrigin", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));           
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITaskListRepository, TaskListRepository>();            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskListService, TaskListService>();

            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<UpdateUserDTO>();
            });

            //AutoMapper
            services.AddAutoMapper(typeof(Mapping));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IKnowTechnology.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
