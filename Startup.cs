using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using AlgorithmProgramingGame_WebApp.Providers;
using AlgorithmProgramingGame_WebApp.Providers.DataModels;
using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services;
using AlgorithmProgramingGame_WebApp.Services.Facade;

namespace AlgorithmProgramingGame_WebApp
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

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
                { configuration.RootPath = "ClientApp/build"; });
            
            // Configure Dependency Injection
            services.Configure<CodeSolutionsDatabaseSettings>(
                Configuration.GetSection(nameof(CodeSolutionsDatabaseSettings)));

            services.AddSingleton<ICodeSolutionsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CodeSolutionsDatabaseSettings>>().Value);
            
            // Services:
            services.AddSingleton<ICodeTaskService, CodeTaskService>();
            services.AddSingleton<IScoreService, ScoreService>();
            services.AddSingleton<ISolutionService, SolutionService>();
            
            // Providers:
            services.AddSingleton<ICodeTaskProvider, CodeTaskProvider>();
            services.AddSingleton<IUserProvider, UserProvider>();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}