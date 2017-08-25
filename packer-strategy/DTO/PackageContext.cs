//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.DTO
{
    using Microsoft.EntityFrameworkCore;
    using Models.Package;

    /// <summary>   A package context. </summary>
    public class PackageContext : DbContext
    {
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        public PackageContext(DbContextOptions<PackageContext> options)
            : base(options)
        {
        }

        /// <summary>   Gets or sets the packages. </summary>
        ///
        /// <value> The packages. </value>
        private DbSet<Package> Packages { get; set; }

        /// <summary>   Gets the packages. </summary>
        ///
        /// <returns>   The packages. </returns>
        public DbSet<Package> GetPackages()
        {
            return Packages;
        }

        /// <summary>   Adds a package. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddPackage(Package item)
        {
            Packages.Add(item);
        }

        /// <summary>   Searches for the first package. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   The found package. </returns>
        public Package FindPackage(string key)
        {
            return Packages.Find(key);
        }

        /// <summary>   Removes the package described by key. </summary>
        ///
        /// <param name="key">  The key. </param>
        public void RemovePackage(string key)
        {
            var entity = Packages.Find(key);
            Packages.Remove(entity);
        }

        /// <summary>   Updates the package described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdatePackage(Package item)
        {
            Packages.Update(item);
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
            modelBuilder.Entity<Package>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Costing>()
                .HasKey(c => new { c.OwnerId, c.RequiredQuantity });
            modelBuilder.Entity<Stage>()
                .HasKey(c => new { c.OwnerId, c.Level });
            modelBuilder.Entity<Models.Plan.Limit>()
                .HasKey(c => new { c.OwnerId, c.StageLevel, c.Index });
        }
    }
}
