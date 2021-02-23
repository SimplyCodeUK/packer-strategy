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
    /// <seealso cref="T:PackIt.DTO.PackItContext{TData}"/>
    public class PlanContext : PackItContext<Plan>
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
        public override IList<Plan> GetAll()
        {
            var ret = new List<Plan>();
            var query = ConstructQuery();

            foreach (var item in query)
            {
                ret.Add(PlanMapper.Convert(item));
            }

            return ret;
        }

        /// <summary> Adds a plan. </summary>
        ///
        /// <param name="item"> The item. </param>
        public override void Add(Plan item)
        {
            var dto = PlanMapper.Convert(item);
            this.Plans.Add(dto);
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
        public override void Remove(string key)
        {
            var entity = this.Plans.Find(key);
            this.Plans.Remove(entity);
        }

        /// <summary> Updates the plan described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public override void Update(Plan item)
        {
            var entity = this.Plans.Find(item.PlanId);
            var dto = PlanMapper.Convert(item);
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

            ConfigureDtoPlan(modelBuilder);
            ConfigureDtoStage(modelBuilder);
            Configure<DtoPlan.DtoLimit>(modelBuilder, "DtoLimit", k => new { k.PlanId, k.StageLevel, k.LimitIndex });
        }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of plans. </returns>
        protected IQueryable<DtoPlan.DtoPlan> ConstructQuery()
        {
            var query = this.Plans
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
