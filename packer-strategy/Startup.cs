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
    /** A startup. */
    public class Startup
    {
        /**
         * Constructor.
         *
         * @param   env The environment.
         */
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /**
         * Gets the configuration.
         *
         * @return  The configuration.
         */
        public IConfigurationRoot Configuration { get; }

        /**
         * Configure services.
         *
         * @param   services    The services.
         */
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PlanContext>(options => options.UseInMemoryDatabase());

            // Add framework services.
            services.AddMvc();

            services.AddSingleton<IPlanRepository, PlanRepository>();
        }

        /**
         * Configures.
         *
         * @param   app             The application.
         * @param   env             The environment.
         * @param   loggerFactory   The logger factory.
         */
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
