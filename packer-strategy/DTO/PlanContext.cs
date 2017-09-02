// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using PackIt.Models.Plan;

    /// <summary>   A plan context. </summary>
    public class PlanContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlanContext" /> class.
        /// </summary>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        public PlanContext(DbContextOptions<PlanContext> options)
            : base(options)
        {
        }

        /// <summary>   Gets or sets the plans. </summary>
        ///
        /// <value> The plans. </value>
        public DbSet<DtoPlan.DtoPlan> Plans { get; set; }

        /// <summary>   Gets the plans. </summary>
        ///
        /// <returns>   The plans. </returns>
        public List<Plan> GetPlans()
        {
            List<Plan> ret = new List<Plan>();

            foreach (DtoPlan.DtoPlan item in this.Plans)
            {
                ret.Add(PlanMapper.Convert(item));
            }

            return ret;
        }

        /// <summary>   Adds a plan. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddPlan(Plan item)
        {
            DtoPlan.DtoPlan dto = PlanMapper.Convert(item);
            this.Plans.Add(dto);
        }

        /// <summary>   Searches for the first plan. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   The found plan. </returns>
        public Plan FindPlan(string key)
        {
            DtoPlan.DtoPlan dto = this.Plans.Find(key);
            Plan ret = dto == null ? null : PlanMapper.Convert(dto);

            return ret;
        }

        /// <summary>   Removes the plan described by key. </summary>
        ///
        /// <param name="key">  The key. </param>
        public void RemovePlan(string key)
        {
            DtoPlan.DtoPlan entity = this.Plans.Find(key);
            this.Plans.Remove(entity);
        }

        /// <summary>   Updates the plan described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdatePlan(Plan item)
        {
            DtoPlan.DtoPlan entity = this.Plans.Find(item.Id);
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
            modelBuilder.Entity<DtoPlan.DtoPlan>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<DtoPlan.DtoStage>()
                .HasKey(c => new { c.PlanId, c.Level });
            modelBuilder.Entity<DtoPlan.DtoLimit>()
                .HasKey(c => new { c.PlanId, c.StageLevel, c.Index });
        }
    }
}
