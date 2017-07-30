//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using Microsoft.EntityFrameworkCore;

namespace packer_strategy.Models
{
    /*!
     * \class   PlanContext
     *
     * \brief   A plan context.
     */
    public class PlanContext : DbContext
    {
        /*!
         * \fn  public PlanContext(DbContextOptions<PlanContext> options) : base(options)
         *
         * \brief   Constructor.
         *
         * \param   options Options for controlling the operation.
         */
        public PlanContext(DbContextOptions<PlanContext> options)
            : base(options)
        {
        }

        /*!
         * \property    public DbSet<Plan.Plan> Strategies
         *
         * \brief   Gets or sets the strategies.
         *
         * \return  The strategies.
         */
        public DbSet<Plan.Plan> Strategies { get; set; }

        /*!
         * \property    public DbSet<Plan.Stage> Stages
         *
         * \brief   Gets or sets the stages.
         *
         * \return  The stages.
         */
        public DbSet<Plan.Stage> Stages { get; set; }

        /*!
         * \fn  protected override void OnModelCreating(ModelBuilder modelBuilder)
         *
         * \brief   Override this method to further configure the model that was discovered by convention
         *          from the entity types exposed in
         *          <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived
         *          context. The resulting model may be cached and re-used for subsequent instances of
         *          your derived context.
         *
         * \param   modelBuilder    The builder being used to construct the model for this context.
         *                          Databases (and other extensions) typically define extension methods on
         *                          this object that allow you to configure aspects of the model that are
         *                          specific to a given database.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan.Plan>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Plan.Stage>()
                .HasKey(c => new { c.StrategyId, c.Level });
        }
    }
}
