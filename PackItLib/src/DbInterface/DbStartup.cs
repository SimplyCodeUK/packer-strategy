// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DbInterface
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using PackIt.Models;

    /// <summary> A start up. </summary>
    [ExcludeFromCodeCoverage]
    public class DbStartup
    {
        /// <summary> The database connection manager. </summary>
        private readonly DbConnectionManager connectionManager;

        /// <summary>
        /// Initialises a new instance of the <see cref="DbStartup" /> class.
        /// </summary>
        ///
        /// <param name="configuration"> Configuration. </param>
        /// <param name="loggerFactory"> Logger factory. </param>
        public DbStartup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = configuration;
            this.LoggerFactory = loggerFactory;

            // Register database contexts.
            // The first context will be the default if not specified in <see cref="Configuration" />
            this.connectionManager = new DbConnectionManager();
            this.connectionManager.RegisterContextBuilder("inmemory", new DbContextBuilderInMemory());
            this.connectionManager.RegisterContextBuilder("postgres", new DbContextBuilderPostgres());
        }

        /// <summary> Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        public IConfiguration Configuration { get; }

        /// <summary> Gets the logger factory. </summary>
        ///
        /// <value> The logger factory. </value>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary> Setup common services. </summary>
        ///
        /// <param name="services"> The services. </param>
        protected void SetupServices(IServiceCollection services)
        {
            // Configure using a sub-section of the appsettings.json file.
            services.Configure<AppSettings>(this.Configuration.GetSection("AppSettings"))
                    .AddApplicationInsightsTelemetry(this.Configuration)
                    .AddApiVersioning();
        }

        /// <summary> Set common app settings. </summary>
        ///
        ///
        /// <param name="app"> The application. </param>
        /// <param name="env"> The environment. </param>
        protected static void SetupApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }
        }

        /// <summary> Adds the database context. </summary>
        ///
        /// <typeparam name="TContext"> The type of the context. </typeparam>
        ///
        /// <param name="services"> The services. </param>
        /// <param name="section"> The section in the configuration. </param>
        protected void AddDbContext<TContext>(IServiceCollection services, string section) where TContext : DbContext
        {
            var connection = this.Configuration.GetSection("Connections").GetSection(section);
            var builder = this.connectionManager.ContextBuilder(connection["Type"]);

            services.AddDbContext<TContext>(builder.CreateContextOptionsBuilder(connection["ConnectionString"]));
        }
    }
}
