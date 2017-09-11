// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using PackIt.DTO;

    /// <summary>   A start up. </summary>
    public class Startup
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Startup" /> class.
        /// </summary>
        ///
        /// <param name="env">  The environment. </param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary>   Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>   Configure services. </summary>
        ///
        /// <param name="services"> The services. </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PlanContext>(options => options.UseInMemoryDatabase("plan"));
            services.AddDbContext<MaterialContext>(options => options.UseInMemoryDatabase("material"));
            services.AddDbContext<PackContext>(options => options.UseInMemoryDatabase("pack"));

            // Add framework services.
            services.AddMvc();
            services.AddApiVersioning();

            services.AddSingleton<IPlanRepository, PlanRepository>();
            services.AddSingleton<IMaterialRepository, MaterialRepository>();
            services.AddSingleton<IPackRepository, PackRepository>();
        }

        /// <summary>   Configures start up. </summary>
        ///
        /// <param name="app">              The application. </param>
        /// <param name="env">              The environment. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
