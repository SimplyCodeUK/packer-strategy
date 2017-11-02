// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using PackItUI.Areas.App.Models;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Plans.DTO;

    /// <summary>   A start up. </summary>
    public class Startup
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Startup" /> class.
        /// </summary>
        ///
        /// <param name="configuration">  The configuration. </param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary> Gets the configuration. </summary>
        /// 
        /// <value> The configuration. </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// This method gets called by the runtime. Use this method to add services to the container.        
        /// </summary>
        /// 
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure using a sub-section of the appsettings.json file.
            services.Configure<AppSettings>(this.Configuration.GetSection("AppSettings"));

            services.AddMvc();

            services.AddSingleton<IMaterialHandler, MaterialHandler>();
            services.AddSingleton<IPackHandler, PackHandler>();
            services.AddSingleton<IPlanHandler, PlanHandler>();
        }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.        
        /// </summary>
        /// 
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("App/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{area=App}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
