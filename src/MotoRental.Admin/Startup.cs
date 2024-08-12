using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoRental.Admin;

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
        services.AddSession();
        services.AddControllersWithViews();

        // services.AddTransient<ITokenService, TokenService>();

        // IAppConfiguration appConfig = new AppConfiguration();

        // var key = Encoding.ASCII.GetBytes(appConfig.Secret);

        // services.AddAuthentication(auth =>
        // {
        //     auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(bearer =>
        // {
        //     bearer.RequireHttpsMetadata = false; //TODO verifica na hora que configurar o https
        //     bearer.SaveToken = true;
        //     bearer.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = new SymmetricSecurityKey(key),
        //         ValidateIssuer = false,
        //         ValidateAudience = false
        //     };
        // });

        services.AddMvc();
        services.AddMemoryCache();
        // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        #region Dependency Injection

        #region Use Cases && Repositories
        // services.AddScoped<ISessionUseCase, SessionUseCase>();
        // services.AddScoped<ISessionRepository, SessionRepository>();
        // services.AddScoped<IUserUseCase, UserUseCase>();
        // services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<IDriverUseCase, DriverUseCase>();
        // services.AddScoped<IDriverRepository, DriverRepository>();
        // services.AddScoped<IDeviceUseCase, DeviceUseCase>();
        // services.AddScoped<IDeviceRepository, DeviceRepository>();
        // services.AddScoped<IGroupUseCase, GroupUseCase>();
        // services.AddScoped<IGroupRepository, GroupRepository>();
        // services.AddScoped<IPositionUseCase, PositionUseCase>();
        // services.AddScoped<IPositionRepository, PositionRepository>();
        // services.AddScoped<IBaseReportUseCase<ReportSummary>, ReportSummaryUseCase>();
        // services.AddScoped<IBaseReportRepository<ReportSummary>, BaseReportRepository<ReportSummary>>();
        // services.AddScoped<IBaseReportUseCase<ReportRoute>, ReportRouteUseCase>();
        // services.AddScoped<IBaseReportRepository<ReportRoute>, BaseReportRepository<ReportRoute>>();
        // services.AddScoped<IBaseReportUseCase<ReportEvent>, ReportEventUseCase>();
        // services.AddScoped<IBaseReportRepository<ReportEvent>, BaseReportRepository<ReportEvent>>();
        // services.AddScoped<IGeoferenceUseCase, GeoferenceUseCase>();
        // services.AddScoped<IGeoferenceRepository, GeoferenceRepository>();
        // services.AddScoped<IMaintenanceUseCase, MaintenanceUseCase>();
        // services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();

        // services.AddScoped<ISessionUtil, SessionUtil>();
        // services.AddScoped<ILogUtil, LogUtil>();

        // services.AddSingleton<IAppConfiguration, AppConfiguration>();
        #endregion

        #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        // loggerFactory.AddFile($"../../Logs/{DateTime.Now}.log");

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseSession();

        // app.Use(async (context, next) =>
        //     {
        //         var token = context.Session.GetString("Token");
        //         if (!string.IsNullOrEmpty(token))
        //         {
        //             context.Request.Headers.Add("Authorization", "Bearer " + token);
        //         }
        //         await next();
        //     });

        app.UseStatusCodePages(context =>
        {
            // var request = context.HttpContext.Request;
            // var response = context.HttpContext.Response;

            // if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            // {
            //     response.Redirect("/Account/Unauthenticated");
            // }

            // if (response.StatusCode == (int)HttpStatusCode.Forbidden)
            // {
            //     response.Redirect("/Account/Forbidden");
            // }

            return System.Threading.Tasks.Task.CompletedTask;
        });

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
