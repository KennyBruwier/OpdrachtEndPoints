using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpdrachtEndPoints
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string WelkomsMsg = $"U bevindt zich in {env.EnvironmentName}";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                WelkomsMsg = "Welkom developer! Veel programmeerplezier!";
            }
            else
            {
                app.UseHsts();
            }
            if (env.IsStaging())
                WelkomsMsg = "Engines on!";
            if (env.IsProduction())
                WelkomsMsg = "Welkom beste klant!";
            app.UseRouting();
            app.UseHttpsRedirection();
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("Images.html");
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions 
            //{ 
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(),"Images")
            //                                            ),
            //    RequestPath = "/Images"
            //} );
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Images")
                                                        ),
                RequestPath="/Images"
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(WelkomsMsg);
                });
            });
        }
    }
}
