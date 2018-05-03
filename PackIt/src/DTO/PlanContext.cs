﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PackIt.Plan;

    /// <summary> A plan context. </summary>
    public class PlanContext : DbContext
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

        /// <summary> Gets the plans. </summary>
        ///
        /// <value> The plans. </value>
        public DbSet<DtoPlan.DtoPlan> Plans { get; private set; }

        /// <summary> Gets the plans. </summary>
        ///
        /// <returns> The plans. </returns>
        public IList<Plan> GetPlans()
        {
            var ret = new List<Plan>();
            var query = this.Plans
                .Include(p => p.Stages)
                .Include(p => p.Stages).ThenInclude(s => s.Limits);

            foreach (DtoPlan.DtoPlan item in query)
            {
                ret.Add(PlanMapper.Convert(item));
            }

            return ret;
        }

        /// <summary> Adds a plan. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddPlan(Plan item)
        {
            DtoPlan.DtoPlan dto = PlanMapper.Convert(item);
            this.Plans.Add(dto);
        }

        /// <summary> Searches for the first plan. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> The found plan. </returns>
        public Plan FindPlan(string key)
        {
            try
            {
                var query = this.Plans
                    .Include(p => p.Stages)
                    .Include(p => p.Stages).ThenInclude(s => s.Limits)
                    .SingleAsync(p => p.PlanId == key);

                query.Wait();
                return PlanMapper.Convert(query.Result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Removes the plan described by key. </summary>
        ///
        /// <param name="key"> The key. </param>
        public void RemovePlan(string key)
        {
            DtoPlan.DtoPlan entity = this.Plans.Find(key);
            this.Plans.Remove(entity);
        }

        /// <summary> Updates the plan described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdatePlan(Plan item)
        {
            DtoPlan.DtoPlan entity = this.Plans.Find(item.PlanId);
            DtoPlan.DtoPlan dto = PlanMapper.Convert(item);
            this.Plans.Remove(entity);
            this.SaveChanges();
            this.Plans.Add(dto);
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

            Configure(modelBuilder.Entity<DtoPlan.DtoPlan>());
            Configure(modelBuilder.Entity<DtoPlan.DtoStage>());
            Configure(modelBuilder.Entity<DtoPlan.DtoLimit>());
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPlan.DtoPlan> builder)
        {
            builder.ToTable("DtoPlan");
            builder.HasKey(p => new { p.PlanId });
            builder
                .HasMany(p => p.Stages)
                .WithOne()
                .HasForeignKey(s => new { s.PlanId });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPlan.DtoStage> builder)
        {
            builder.ToTable("DtoStage");
            builder.HasKey(s => new { s.PlanId, s.StageLevel });
            builder
                .HasMany(s => s.Limits)
                .WithOne()
                .HasForeignKey(l => new { l.PlanId, l.StageLevel });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPlan.DtoLimit> builder)
        {
            builder.ToTable("DtoLimit");
            builder.HasKey(l => new { l.PlanId, l.StageLevel, l.LimitIndex });
        }
    }
}