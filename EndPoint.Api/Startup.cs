using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint.Api
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
            services.AddAutoMapper(cfg =>
            {
                cfg.ValidateInlineMaps = false;
            }, typeof(Startup));

            //services.AddSpaStaticFiles();

            services.AddControllers();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EndPoint.Api", Version = "v1" });
            //});

            services.AddControllersWithViews();

            //services.AddCors();

            // configure jwt authentication            
            var key = Encoding.ASCII.GetBytes(VaribleForName.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IUserFacad, UserFacad>();
            services.AddScoped<ISecurityFacad, SecurityFacad>();
            services.AddScoped<IBasicInfoFacad, BasicInfoFacad>();

            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(VaribleForName.MainConnectionString));
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EndPoint.Api v1"));

                //app.UseCors(p => p.WithOrigins("http://127.0.0.1:4200").AllowAnyMethod().AllowAnyHeader());
            }
            else
            {
                app.UseResponseCompression();
                //app.UseCors(p => p.WithOrigins("https://sellerchi.ir").AllowAnyMethod().AllowAnyHeader());
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseStaticFiles();
            app.UseDefaultFiles();

            //app.Use(async (context, next) =>
            //{
            //    // Do work that can write to the Response.
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.
            //});

            //app.UseSpaStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                        name: "areas",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            //app
            //    .Map("/admin", admin =>
            //    {
            //        admin.UseSpa(spa =>
            //        {
            //            spa.Options.SourcePath = "wwwroot/admin";
            //            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            //            {
            //                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin"))
            //            };
            //        });
            //    })
            //    .Map("/customer", client =>
            //    {
            //        client.UseSpa(spa =>
            //        {
            //            spa.Options.SourcePath = "wwwroot/customer";
            //            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            //            {
            //                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/customer"))
            //            };
            //        });
            //    })
            //    .Map("/superVisor", client =>
            //    {
            //        client.UseSpa(spa =>
            //        {
            //            spa.Options.SourcePath = "wwwroot/superVisor";
            //            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            //            {
            //                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/superVisor"))
            //            };
            //        });
            //    })
            //.Map("", client =>
            //{
            //    client.UseSpa(spa =>
            //    {
            //        spa.Options.SourcePath = "wwwroot/";
            //        spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            //        {
            //            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/"))
            //        };
            //    });
            //});

        }
    }
}
