//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using Microsoft.EntityFrameworkCore;

namespace packer_strategy.Models
{
    public class StrategyContext : DbContext
    {
        public StrategyContext(DbContextOptions<StrategyContext> options)
            : base(options)
        {
        }

        public DbSet<Strategy> Strategies { get; set; }
        public DbSet<Stage> Stages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Strategy>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Stage>()
                .HasKey(c => new { c.StrategyId, c.Level });
        }
    }
}
