// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
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
        private DbSet<Plan> Plans { get; set; }

        /// <summary>   Gets the plans. </summary>
        ///
        /// <returns>   The plans. </returns>
        public DbSet<Plan> GetPlans()
        {
            return this.Plans;
        }

        /// <summary>   Adds a plan. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddPlan(Plan item)
        {
            this.Plans.Add(item);
        }

        /// <summary>   Searches for the first plan. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   The found plan. </returns>
        public Plan FindPlan(string key)
        {
            return this.Plans.Find(key);
        }

        /// <summary>   Removes the plan described by key. </summary>
        ///
        /// <param name="key">  The key. </param>
        public void RemovePlan(string key)
        {
            Plan entity = this.Plans.Find(key);
            this.Plans.Remove(entity);
        }

        /// <summary>   Updates the plan described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdatePlan(Plan item)
        {
            this.Plans.Update(item);
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
            modelBuilder.Entity<Plan>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Stage>()
                .HasKey(c => new { c.OwnerId, c.Level });
            modelBuilder.Entity<Limit>()
                .HasKey(c => new { c.OwnerId, c.StageLevel, c.Index });
        }
    }
}
