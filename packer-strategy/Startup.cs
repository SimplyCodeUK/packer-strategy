//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using packer_strategy.Models;

namespace packer_strategy
{
    /// <summary>   A startup. </summary>
    public class Startup
    {
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="env">  The environment. </param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
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
            services.AddDbContext<PlanContext>(options => options.UseInMemoryDatabase());

            // Add framework services.
            services.AddMvc();

            services.AddSingleton<IPlanRepository, PlanRepository>();
        }

        /// <summary>   Configures. </summary>
        ///
        /// <param name="app">              The application. </param>
        /// <param name="env">              The environment. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
