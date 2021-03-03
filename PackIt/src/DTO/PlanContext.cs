// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PackIt.Plan;

    /// <summary> A plan context. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.PackItContext{TData, TDtoData, TMapper}"/>
    public class PlanContext : PackItContext<Plan, DtoPlan.DtoPlan, PlanMapper>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlanContext" /> class.
        /// </summary>
        ///
        /// <param name="options"> Options for controlling the operation. </param>
        public PlanContext(DbContextOptions<PlanContext> options)
            : base(options)
        {
        }

        /// <summary> Searches for the first plan. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> The found plan. </returns>
        public override Plan Find(string key)
        {
            try
            {
                var query = ConstructQuery().SingleAsync(p => p.PlanId == key);

                query.Wait();
                return this.Mapper.ConvertToData(query.Result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Updates the plan described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public override void Update(Plan item)
        {
            var entity = this.Resources.Find(item.PlanId);
            var dto = this.Mapper.ConvertToDto(item);
            this.Resources.Remove(entity);
            this.SaveChanges();
            this.Resources.Add(dto);
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention
        /// from the entity types exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" />
        /// properties on your derived context. The resulting model may be cached and re-used for
        /// subsequent instances of your derived context.
        /// </summary>
        ///
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        ///
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context.
        /// Databases (and other extensions) typically define extension
        /// methods on this object that allow you to configure aspects of the
        /// model that are specific to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureDtoPlan(modelBuilder);
            ConfigureDtoStage(modelBuilder);
            Configure<DtoPlan.DtoLimit>(modelBuilder, "DtoLimit", k => new { k.PlanId, k.StageLevel, k.LimitIndex });
        }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of plans. </returns>
        protected override IQueryable<DtoPlan.DtoPlan> ConstructQuery()
        {
            var query = this.Resources
                .Include(p => p.Stages)
                .Include(p => p.Stages).ThenInclude(s => s.Limits);

            return query;
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoPlan(ModelBuilder modelBuilder)
        {
            Configure<DtoPlan.DtoPlan>(modelBuilder, "DtoPlan", k => new { k.PlanId })
                .HasMany(p => p.Stages)
                .WithOne()
                .HasForeignKey(s => new { s.PlanId });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoStage(ModelBuilder modelBuilder)
        {
            Configure<DtoPlan.DtoStage>(modelBuilder, "DtoStage", k => new { k.PlanId, k.StageLevel })
                .HasMany(s => s.Limits)
                .WithOne()
                .HasForeignKey(l => new { l.PlanId, l.StageLevel });
        }
    }
}
