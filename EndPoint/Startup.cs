using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace EndPoint
{
    public class Startup
    {

        public static Dictionary<string, string> redirectList = new Dictionary<string, string>();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAutoMapper(cfg =>
            {
                cfg.ValidateInlineMaps = false;
            }, typeof(Startup));

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

            services.AddScoped<IValidator<RequestReferencesDto>, ValidatorRequestReferencesDto>();            

            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(VaribleForName.MainConnectionString));

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
                app.UseCors(p => p.WithOrigins("https://test.rayshomar.ir").AllowAnyMethod().AllowAnyHeader());
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                        endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );

            });

            app.UseMiddleware<EndRequestMiddleware>();


            ///////////Important LowerCase , Dest Location
            redirectList.Add("contact", "ContactUs");
            redirectList.Add("درباره-ما", "Page/AboutUs");
            redirectList.Add("ساختار-مدیریت", "Page/ManagerOfParsKyan");
            redirectList.Add("رتبه-بندی-اعتباری-پارس", "Page/رتبه_بندی_اعتباری");
            redirectList.Add("مشتریان-عمده", "Page/مشتریان_عمده");
            redirectList.Add("نمودار-سازمانی", "Page/OrganazationChart");
        }
}

internal class EndRequestMiddleware {
    private readonly RequestDelegate _next;


    public EndRequestMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke(HttpContext context) {
        // Do tasks before other middleware here, aka 'BeginRequest'
        // ...
        string clearpath = context.Request.Path.Value.ToLower().Replace("/", "");
        if(Startup.redirectList.ContainsKey(clearpath)) {
            context.Response.StatusCode = 301;
            context.Response.Headers.Add("Location", "/" + Startup.redirectList[clearpath]);
        } else {
            await _next(context);
        }
    }
}
}
