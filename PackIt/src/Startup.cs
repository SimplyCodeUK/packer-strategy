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
    using Microsoft.Extensions.Logging;
    using PackIt.DbInterface;
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
        /// <param name="loggerFactory"> Logger factory. </param>
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
            : base(configuration, loggerFactory)
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
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=About}/{action=Get}/{id?}");
            });

            if (env.IsDevelopment())
            {
                Seed(app);
            }
        }

        private static void ReadData<TContext, TData, TDtoData, TMapper>(IServiceScope serviceScope, string filename)
            where TData : class
            where TDtoData : class
            where TContext : PackItContext<TData, TDtoData, TMapper>
            where TMapper : IPackItMapper<TData, TDtoData>, new()
        {
            var context = serviceScope.ServiceProvider.GetService<TContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Resources.AnyAsync().Wait();
            if (!context.Resources.AnyAsync().Result)
            {
                var text = File.ReadAllText(filename);
                foreach (var item in JsonSerializer.Deserialize<List<TData>>(text))
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
    }
}
