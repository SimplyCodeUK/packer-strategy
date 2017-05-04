using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
