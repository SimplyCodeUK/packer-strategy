// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IDrawingRepository, DrawingRepository>()
                    .AddMvc(options => options.EnableEndpointRouting = false)
                    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            this.AddDbContext<DrawingContext>(services, "DrawingContext");
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

            _ = app.UseHttpsRedirection()
               .UseAuthorization()
               .UseRouting()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            InitialiseDatabases(app);
        }

        /// <summary> Initialise a data base. Read data into development environment and ensure it is created. </summary>
        ///
        /// <typeparam name="TContext"> The context. </typeparam>
        /// <typeparam name="TData"> The data to store. </typeparam>
        /// <typeparam name="TDtoData"> The DTO for the data. </typeparam>
        /// <typeparam name="TMapper"> The mapper to convert data to DTO and vice versa. </typeparam>
        ///
        /// <param name="serviceScope"> The service scope. </param>
        private static void InitialiseDatabase<TContext, TData, TDtoData, TMapper>(IServiceScope serviceScope)
            where TData : class
            where TDtoData : class
            where TContext : PackItContext<TData, TDtoData, TMapper>
            where TMapper : IPackItMapper<TData, TDtoData>, new()
        {
            var context = serviceScope.ServiceProvider.GetService<TContext>();
            _ = context.Database.EnsureDeleted();
            _ = context.Database.EnsureCreated();
        }

        /// <summary> Initialise the databases. </summary>
        ///
        /// <param name="app"> The application. </param>
        private static void InitialiseDatabases(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            // Seed Drawings database
            InitialiseDatabase<DrawingContext, PackIt.Drawing.Drawing, PackIt.DTO.DtoDrawing.DtoDrawing, DrawingMapper>(serviceScope);
        }
    }
}
