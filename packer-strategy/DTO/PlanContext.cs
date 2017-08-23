﻿//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO
{
    using Microsoft.EntityFrameworkCore;
    using Models.Plan;

    /// <summary>   A plan context. </summary>
    public class PlanContext : DbContext
    {
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        public PlanContext(DbContextOptions<PlanContext> options)
            : base(options)
        {
        }

        /// <summary>   Gets or sets the plans. </summary>
        ///
        /// <value> The plans. </value>
        public DbSet<Plan> Plans
        {
            get; set;
        }

        /// <summary>   Gets or sets the stages. </summary>
        ///
        /// <value> The stages. </value>
        public DbSet<Stage> Stages
        {
            get; set;
        }

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention
        ///     from the entity types exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" />
        ///     properties on your derived context. The resulting model may be cached and re-used for
        ///     subsequent instances of your derived context.
        /// </summary>
        ///
        /// <remarks>
        ///     If a model is explicitly set on the options for this context (via
        ///     <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        ///     then this method will not be run.
        /// </remarks>
        ///
        /// <param name="modelBuilder"> The builder being used to construct the model for this context.
        ///                             Databases (and other extensions) typically define extension
        ///                             methods on this object that allow you to configure aspects of the
        ///                             model that are specific to a given database. </param>
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