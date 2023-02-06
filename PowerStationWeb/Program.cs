using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NLog.Extensions.Logging;
using PowerStationDomain.models;
using PowerStationInfrastructure.helpers;
using PowerStationInfrastructure.Services;
using PowerStationWeb.Data;
using Syncfusion.Blazor;

namespace PowerStationWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@32302e342e30bmC3fG4/CSaB/VPFpWp/Dk/7ILyXh5TtZ6id50Hku48=");
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<AdConfiguration>(c=>new AdConfiguration("localhost"));
            builder.Services.AddSingleton<Configuration>();
            builder.Services.AddScoped<IAdServiceUser,AdServiceUser>();
            builder.Services.AddScoped<IAdComputerService, AdComputerService>();
            builder.Services.AddScoped<IAdServiceSecurity, AdServiceGroup>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddNLog("Nlog.config");
                builder.AddConsole();

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}