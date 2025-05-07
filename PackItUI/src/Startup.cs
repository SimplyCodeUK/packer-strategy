// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PackItLib.DbInterface;
    using PackItUI.Areas.Common.DTO;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Plans.DTO;
    using PackItUI.Areas.Uploads.DTO;

    /// <summary> A start up. </summary>
    ///
    /// <seealso cref="DbStartup" />
    [ExcludeFromCodeCoverage]
    public class Startup : DbStartup
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Startup" /> class.
        /// </summary>
        ///
        /// <param name="configuration"> Configuration. </param>
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <summary> This method gets called by the runtime. Use this method to add services to the container. </summary>
        ///
        /// <param name="services"> The services. </param>
        ///
        /// <seealso cref="DbStartup.SetupServices" />
        public void ConfigureServices(IServiceCollection services)
        {
            this.SetupServices(services);
            services.AddSingleton<DbServiceHandler<PackItLib.Material.Material>, MaterialHandler>()
                    .AddSingleton<DbServiceHandler<PackItLib.Pack.Pack>, PackHandler>()
                    .AddSingleton<DbServiceHandler<PackItLib.Plan.Plan>, PlanHandler>()
                    .AddSingleton<IUploadHandler, UploadHandler>()
                    .AddSingleton<DrawHandler, DrawHandler>()
                    .AddMvc(options => options.EnableEndpointRouting = false)
                    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
        }

        /// <summary> This method gets called by the runtime. Use this method to configure the HTTP request pipeline. </summary>
        ///
        /// <param name="app"> The application. </param>
        /// <param name="env"> The environment. </param>
        ///
        /// <seealso cref="DbStartup.SetupApp" />
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SetupApp(app, env);
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/App/Home/Error");
            }

            app.UseStaticFiles()
               .UseMvc(routes =>
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
