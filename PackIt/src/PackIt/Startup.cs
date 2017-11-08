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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using PackIt.DTO;
    using PackIt.Models;

    /// <summary> A start up. </summary>
    public class Startup
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Startup" /> class.
        /// </summary>
        ///
        /// <param name="env"> The environment. </param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary> Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary> Configure services. </summary>
        ///
        /// <param name="services"> The services. </param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure using a sub-section of the appsettings.json file.
            services.Configure<AppSettings>(this.Configuration.GetSection("AppSettings"));

            services.AddDbContext<PlanContext>(options => options.UseInMemoryDatabase("plan"));
            services.AddDbContext<MaterialContext>(options => options.UseInMemoryDatabase("material"));
            services.AddDbContext<PackContext>(options => options.UseInMemoryDatabase("pack"));

            // Add framework services.
            services.AddMvc();
            services.AddApiVersioning();

            services.AddSingleton<IMaterialRepository, MaterialRepository>();
            services.AddSingleton<IPackRepository, PackRepository>();
            services.AddSingleton<IPlanRepository, PlanRepository>();
        }

        /// <summary> Configures start up. </summary>
        ///
        /// <param name="app"> The application. </param>
        /// <param name="env"> The environment. </param>
        /// <param name="loggerFactory"> The logger factory. </param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            if (env.IsDevelopment())
            {
                Seed(app);
            }
        }

        /// <summary> Seeds the specified application. </summary>
        ///
        /// <param name="app"> The application. </param>
        private static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                            .CreateScope())
            {
                // Seed Pack database
                var packContext = serviceScope.ServiceProvider.GetService<PackContext>();
                packContext.Database.EnsureDeleted();
                packContext.Database.EnsureCreated();

                var asyncTask = packContext.Packs.AnyAsync();
                asyncTask.Wait();
                if (!asyncTask.Result)
                {
                    string text = File.ReadAllText("Seeds/pack.json");
                    foreach (var item in JsonConvert.DeserializeObject<List<Pack.Pack>>(text))
                    {
                        packContext.AddPack(item);
                    }
                }

                packContext.SaveChanges();

                // Seed Plan database
                var planContext = serviceScope.ServiceProvider.GetService<PlanContext>();
                planContext.Database.EnsureDeleted();
                planContext.Database.EnsureCreated();

                asyncTask = planContext.Plans.AnyAsync();
                asyncTask.Wait();
                if (!asyncTask.Result)
                {
                    string text = File.ReadAllText("Seeds/plan.json");
                    foreach (var item in JsonConvert.DeserializeObject<List<Plan.Plan>>(text))
                    {
                        planContext.AddPlan(item);
                    }
                }

                planContext.SaveChanges();

                // Seed Material database
                var materialContext = serviceScope.ServiceProvider.GetService<MaterialContext>();
                materialContext.Database.EnsureDeleted();
                materialContext.Database.EnsureCreated();

                asyncTask = materialContext.Materials.AnyAsync();
                asyncTask.Wait();
                if (!asyncTask.Result)
                {
                    string text = File.ReadAllText("Seeds/material.json");
                    foreach (var item in JsonConvert.DeserializeObject<List<Material.Material>>(text))
                    {
                        materialContext.AddMaterial(item);
                    }
                }

                materialContext.SaveChanges();
            }
        }
    }
}
