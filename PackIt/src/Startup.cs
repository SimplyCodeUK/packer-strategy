// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text.Json;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PackItLib.DbInterface;
    using PackIt.DTO;

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
            services.AddScoped<IMaterialRepository, MaterialRepository>()
                    .AddScoped<IPackRepository, PackRepository>()
                    .AddScoped<IPlanRepository, PlanRepository>()
                    .AddMvc(options => options.EnableEndpointRouting = false)
                    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            this.AddDbContext<MaterialContext>(services, "MaterialContext");
            this.AddDbContext<PackContext>(services, "PackContext");
            this.AddDbContext<PlanContext>(services, "PlanContext");
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
            _ = app.UseMvc(routes =>
              {
                  routes.MapRoute(
                      name: "default",
                      template: "{controller=About}/{action=Get}/{id?}");
              });

            InitialiseDatabases(app, env);
        }

        /// <summary> Initialise a data base. Read data into development environment and ensure it is created. </summary>
        ///
        /// <typeparam name="TContext"> The context. </typeparam>
        /// <typeparam name="TData"> The data to store. </typeparam>
        /// <typeparam name="TDtoData"> The DTO for the data. </typeparam>
        /// <typeparam name="TMapper"> The mapper to convert data to DTO and vice versa. </typeparam>
        ///
        /// <param name="serviceScope"> The service scope. </param>
        /// <param name="env"> The environment. </param>
        /// <param name="filename"> Data file name for development environment. </param>
        private static void InitialiseDatabase<TContext, TData, TDtoData, TMapper>(IServiceScope serviceScope, IWebHostEnvironment env, string filename)
            where TData : class
            where TDtoData : class
            where TContext : PackItLib.DTO.PackItContext<TData, TDtoData, TMapper>
            where TMapper : PackItLib.DTO.IPackItMapper<TData, TDtoData>, new()
        {
            var context = serviceScope.ServiceProvider.GetService<TContext>();

            // Only reconstruct the database when in development
            if (env.IsDevelopment())
            {
                _ = context.Database.EnsureDeleted();
            }
            _ = context.Database.EnsureCreated();
            context.Resources.AnyAsync().Wait();

            // Only reconstruct the database when in development
            if (env.IsDevelopment())
            {
                if (!context.Resources.AnyAsync().Result)
                {
                    var text = File.ReadAllText(filename);
                    foreach (var item in JsonSerializer.Deserialize<List<TData>>(text))
                    {
                        context.Add(item);
                    }
                }
            }
            _ = context.SaveChanges();
        }

        /// <summary> Initialise the databases. </summary>
        ///
        /// <param name="app"> The application. </param>
        /// <param name="env"> The environment. </param>
        private static void InitialiseDatabases(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            // Initialise Pack database
            InitialiseDatabase<PackContext, PackItLib.Pack.Pack, PackItLib.DTO.DtoPack.DtoPack, PackMapper>(serviceScope, env, "Seeds/pack.json");

            // Initialise Plan database
            InitialiseDatabase<PlanContext, PackItLib.Plan.Plan, PackItLib.DTO.DtoPlan.DtoPlan, PlanMapper>(serviceScope, env, "Seeds/plan.json");

            // Initialise Material database
            InitialiseDatabase<MaterialContext, PackItLib.Material.Material, PackItLib.DTO.DtoMaterial.DtoMaterial, MaterialMapper>(serviceScope, env, "Seeds/material.json");
        }
    }
}
