// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using PackIt.DbInterface;
    using PackIt.DTO;
    using PackIt.Models;

    /// <summary> A start up. </summary>
    public class Startup
    {
        /// <summary> The database connection manager. </summary>
        private readonly DbConnectionManager connectionManager;

        /// <summary>
        /// Initialises a new instance of the <see cref="Startup" /> class.
        /// </summary>
        ///
        /// <param name="env"> The environment. </param>
        /// <param name="configuration"> Configuration. </param>
        /// <param name="loggerFactory"> Logger factory. </param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.HostingEnvironment = env;
            this.Configuration = configuration;
            this.LoggerFactory = loggerFactory;

            // Register database contexts.
            // The first context will be the default if not specified in <see cref="Configuration" />
            this.connectionManager = new DbConnectionManager();
            this.connectionManager.RegisterContextBuilder("inmemory", new DbContextBuilderInMemory());
            this.connectionManager.RegisterContextBuilder("postgres", new DbContextBuilderPostgres());
        }

        /// <summary> Gets the hosting environment. </summary>
        ///
        /// <value> The hosting environment. </value>
        public IWebHostEnvironment HostingEnvironment { get; }

        /// <summary> Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        public IConfiguration Configuration { get; }

        /// <summary> Gets the logger factory. </summary>
        ///
        /// <value> The logger factory. </value>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary> Configure services. </summary>
        ///
        /// <param name="services"> The services. </param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure using a sub-section of the appsettings.json file.
            services.Configure<AppSettings>(this.Configuration.GetSection("AppSettings"));

            services.AddApplicationInsightsTelemetry(this.Configuration);

            this.AddDbContext<MaterialContext>(services, "MaterialContext");
            this.AddDbContext<PackContext>(services, "PackContext");
            this.AddDbContext<PlanContext>(services, "PlanContext");

            // Add framework services.
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddApiVersioning();

            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IPackRepository, PackRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
        }

        /// <summary> Configures start up. </summary>
        ///
        /// <param name="app"> The application. </param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=About}/{action=Get}/{id?}");
            });
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if ( HostingEnvironment.IsDevelopment() )
            {
                Seed(app);
            }
        }

        private static void ReadData<TContext, TData, TDtoData, TMapper>(IServiceScope serviceScope, string filename)
            where TData : class
            where TDtoData : class
            where TContext : PackItContext<TData, TDtoData, TMapper>
            where TMapper : PackItMapper<TData, TDtoData>, new()
        {
            var context = serviceScope.ServiceProvider.GetService<TContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Resources.AnyAsync().Wait();
            if (!context.Resources.AnyAsync().Result)
            {
                var text = File.ReadAllText(filename);
                foreach (var item in JsonConvert.DeserializeObject<List<TData>>(text))
                {
                    context.Add(item);
                }
            }
            context.SaveChanges();
        }

        /// <summary> Seeds the specified application. </summary>
        ///
        /// <param name="app"> The application. </param>
        private static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            // Seed Pack database
            ReadData<PackContext, Pack.Pack, DTO.DtoPack.DtoPack, PackMapper>(serviceScope, "Seeds/pack.json");

            // Seed Plan database
            ReadData<PlanContext, Plan.Plan, DTO.DtoPlan.DtoPlan, PlanMapper>(serviceScope, "Seeds/plan.json");

            // Seed Material database
            ReadData<MaterialContext, Material.Material, DTO.DtoMaterial.DtoMaterial, MaterialMapper>(serviceScope, "Seeds/material.json");
        }

        /// <summary> Adds the database context. </summary>
        ///
        /// <typeparam name="TContext"> The type of the context. </typeparam>
        ///
        /// <param name="services"> The services. </param>
        /// <param name="section"> The section in the configuration. </param>
        private void AddDbContext<TContext>(IServiceCollection services, string section) where TContext : DbContext
        {
            var connection = this.Configuration.GetSection("Connections").GetSection(section);
            var builder = this.connectionManager.ContextBuilder(connection["Type"]);

            services.AddDbContext<TContext>(builder.CreateContextOptionsBuilder(connection["ConnectionString"]));
        }
    }
}
