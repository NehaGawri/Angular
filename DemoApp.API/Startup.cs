using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DemoApp.API.Models.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using DemoApp.API.Helpers;
using AutoMapper;

namespace DemoApp.API
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
            services.AddDbContext<DataContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers().AddNewtonsoftJson(opt=>
            {
            opt.SerializerSettings.ReferenceLoopHandling= Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddAutoMapper(typeof(DataRepository).Assembly );
            services.AddCors();
            services.AddScoped<IDataRepository,DataRepository>();
            services.AddScoped<IAuthRepository,AuthRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                                        AddJwtBearer(
                                            options => 
                                            {
                                                options.TokenValidationParameters= new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                                {
                                                    ValidateIssuerSigningKey=true,
                                                    IssuerSigningKey= new SymmetricSecurityKey
                                                    (System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                                                    ValidateIssuer=false,
                                                    ValidateAudience=false
                                                };
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
                app.UseExceptionHandler(builder=>{
                    builder.Run(async context =>{
                        context.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                        var error =context.Features.Get<IExceptionHandlerFeature>();
                        if(error!=null)
                        {
                            context.Response.AddExceptionMessage(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
             endpoints.MapControllers();
            });
        }
    }
}
