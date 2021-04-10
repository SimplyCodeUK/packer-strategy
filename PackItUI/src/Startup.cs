// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using PackIt.Models;
    using PackItUI.Areas.Common.DTO;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Plans.DTO;
    using PackItUI.Areas.Uploads.DTO;
    using System.Diagnostics.CodeAnalysis;

    /// <summary> A start up. </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Startup" /> class.
        /// </summary>
        ///
        /// <param name="configuration"> Configuration. </param>
        /// <param name="loggerFactory"> Logger factory. </param>
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = configuration;
            this.LoggerFactory = loggerFactory;
        }

        /// <summary> Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        public IConfiguration Configuration { get; }

        /// <summary> Gets the logger factory. </summary>
        ///
        /// <value> The logger factory. </value>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary> This method gets called by the runtime. Use this method to add services to the container. </summary>
        ///
        /// <param name="services"> The services. </param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure using a sub-section of the appsettings.json file.
            services.Configure<AppSettings>(this.Configuration.GetSection("AppSettings"))
                    .AddApplicationInsightsTelemetry(this.Configuration)
                    .AddSingleton<DbServiceHandler<PackIt.Material.Material>, MaterialHandler>()
                    .AddSingleton<DbServiceHandler<PackIt.Pack.Pack>, PackHandler>()
                    .AddSingleton<DbServiceHandler<PackIt.Plan.Plan>, PlanHandler>()
                    .AddSingleton<IUploadHandler, UploadHandler>()
                    .AddMvc(options => options.EnableEndpointRouting = false);
        }

        /// <summary> This method gets called by the runtime. Use this method to configure the HTTP request pipeline. </summary>
        ///
        /// <param name="app"> The application. </param>
        /// <param name="env"> The environment. </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/App/Home/Error");
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
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}
