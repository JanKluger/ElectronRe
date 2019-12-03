using System;
using System.Threading.Tasks;
using Test.Services;
using ElectronNET.API;
using ElectronNET.API.Entities;
using ElectronRe.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElectronRe
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //if (!env.IsDevelopment())
            //{
                app.UseSpaStaticFiles();
            //}

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                }

            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    //app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //app.UseStaticFiles();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            //RegisterMessageHandlers();
            //// Open the Electron-Window here
            //Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());

            Bootstrap();
        }

        private async void Bootstrap()
        {
            Console.WriteLine($"Starting Bootstrap - ElectronActive: {HybridSupport.IsElectronActive}");

            if (HybridSupport.IsElectronActive)
            {
                RegisterMessageHandlers();

                WebPreferences wp = new WebPreferences();
                wp.NodeIntegration = true;
                wp.AllowRunningInsecureContent = true;
                var options = new BrowserWindowOptions()
                {
                    //Show = false,
                    AutoHideMenuBar = true,
                    Width = 1440,
                    Height = 900,
                    WebPreferences = wp
                };

                var mainWindow = await Electron.WindowManager.CreateWindowAsync(options);
                Console.WriteLine($"Main window created");
                MessagingService.MainBrowserWindow = mainWindow;

                mainWindow.OnReadyToShow += () =>
                {
                    Console.WriteLine($"Showing main window");
                    mainWindow.Show();
                };
            }
        }

        private void RegisterMessageHandlers()
        {
            Console.WriteLine($"Registering handlers");
            MessagingService.Subscribe("TestGet", s =>
            {
                var service = new TestService();
                service.GetTestData(s);
            });
        }
    }
}
