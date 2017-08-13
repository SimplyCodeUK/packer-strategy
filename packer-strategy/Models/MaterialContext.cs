//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using Microsoft.EntityFrameworkCore;

namespace packer_strategy.Models
{
    /// <summary>   A material context. </summary>
    public class MaterialContext : DbContext
    {
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        public MaterialContext(DbContextOptions<MaterialContext> options)
            : base(options)
        {
        }

        /// <summary>   Gets or sets the materials. </summary>
        ///
        /// <value> The materials. </value>
        public DbSet<Material.Material> Materials { get; set; }

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
            modelBuilder.Entity<Material.Material>()
                .HasKey(c => new { c.IdType, c.Id });
            modelBuilder.Entity<Material.Costing>()
                .HasKey(c => new { c.MaterialIdType, c.MaterialId, c.Quantity });
        }
    }
}
