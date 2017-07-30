//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using Microsoft.EntityFrameworkCore;

namespace packer_strategy.Models
{
    /*! A plan context. */
    public class PlanContext : DbContext
    {
        /*!
         * Constructor.
         *
         * @param   options Options for controlling the operation.
         */
        public PlanContext(DbContextOptions<PlanContext> options)
            : base(options)
        {
        }

        /*!
         * Gets or sets the plans.
         *
         * @return  The plans.
         */
        public DbSet<Plan.Plan> Plans { get; set; }

        /*!
         * Gets or sets the stages.
         *
         * @return  The stages.
         */
        public DbSet<Plan.Stage> Stages { get; set; }

        /*!
         * Override this method to further configure the model that was discovered by convention from
         * the entity types exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties
         * on your derived context. The resulting model may be cached and re-used for subsequent
         * instances of your derived context.
         *
         * @param   modelBuilder    The builder being used to construct the model for this context.
         *                          Databases (and other extensions) typically define extension methods on
         *                          this object that allow you to configure aspects of the model that are
         *                          specific to a given database.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan.Plan>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Plan.Stage>()
                .HasKey(c => new { c.StrategyId, c.Level });
        }
    }
}
