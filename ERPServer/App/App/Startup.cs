using System;
using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using App.Helper;
using Microsoft.Extensions.FileProviders;
using NLog;
using Infrastructure.ExceptionHandling.ExceptionMiddlewareExtensions;
using Data.Entities.UserManagement;
using Account.DataServiceLayer.Contracts;
using Account.DataServiceLayer.Handlers;
using Infrastructure.Notifications.Contracts;
using Infrastructure.Notifications.Handlers;
using CorePush.Google;
using CorePush.Apple;
using Infrastructure.Notifications.Models;
using Microsoft.OpenApi.Models;
using Entities.Account;
using IdentityServer4.Stores;
//using Telerik.Reporting.Cache.File;
//using Telerik.Reporting.Services;
//using Telerik.Reporting.Services.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //#region telerik report

            //// Configure dependencies for ReportsController.
            //services.TryAddSingleton<IReportServiceConfiguration>(sp =>
            //    new ReportServiceConfiguration
            //    {
            //        // The default ReportingEngineConfiguration will be initialized from appsettings.json or appsettings.{EnvironmentName}.json:
            //        ReportingEngineConfiguration = sp.GetService<IConfiguration>(),
            //        // In case the ReportingEngineConfiguration needs to be loaded from a specific configuration file, use the approach below:
            //        // NOTE: Configuration key (ReportingCashPath) must be shanged in appsettings.json from one app to another specially if we have multiple apps hosted on the same server
            //        HostAppId = Configuration.GetSection("ReportingCashPath").Value,
            //        Storage = new FileStorage(Configuration.GetSection("ReportingCashPath").Value),
            //        ReportResolver = new ReportTypeResolver()
            //            .AddFallbackResolver(new ReportFileResolver(Path.Combine(sp.GetService<IHostingEnvironment>().ContentRootPath, "Reports"))),
            //        ClientSessionTimeout = 30,
            //        ReportSharingTimeout = 0
            //    });


            //#endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            var emailConfig = Configuration
               .GetSection("EmailConfiguration")
               .Get<EmailConfigurationDTO>();
            services.AddSingleton(emailConfig);



            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});


            services.AddCors();

            //>>>>> Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //>>>>End Auto Mapper Configurations

            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddControllers()
             .AddNewtonsoftJson(
                   options =>
                   {
                       options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   });
            //>>>Add JWT Authentication And DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient);

            services.AddIdentity<AppUser, AppRole>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            services.AddIdentityServer()
                 .AddInMemoryCaching()
                 .AddClientStore<InMemoryClientStore>()
                 .AddResourceStore<InMemoryResourcesStore>()
                .AddDeveloperSigningCredential()
               // this adds the operational data from DB (codes, tokens, consents)
               .AddOperationalStore(options =>
               {
                   options.ConfigureDbContext = builder => builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                   // this enables automatic token cleanup. this is optional.
                   options.EnableTokenCleanup = true;
                   options.TokenCleanupInterval = 30; // interval in seconds
               })
               .AddAspNetIdentity<AppUser>();

            // >>> END Add JWT Authentication And DbContext

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            DependencyInjection.AddTransient(services);

            #region Firebase
            services.AddTransient<INotificationService, NotificationService>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("FcmNotification");
            services.Configure<FcmNotificationSetting>(appSettingsSection);

            // Register the swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "V1", new OpenApiInfo { Title = "My API", Version = "V1" });
            });
            #endregion

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
                //app.UseExceptionHandler("/Error");
                app.Use(async (context, next) =>
                {
                    await next();
                    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = "/index.html";
                        await next();
                    }
                });
                app.UseHsts();
            }
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            app.UseAuthentication();//JWT Auth

            //app.UseIdentityServer();//Add IdentityServer to our request processing pipeline
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images")),
                RequestPath = "/wwwroot/Images"
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images")),
                RequestPath = "/wwwroot/Images"
            });

            #region Firbase 
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable the SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/V1/swagger.json", name: "Base APIs V1");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #endregion


            app.UseMvc();
        }
    }
}
