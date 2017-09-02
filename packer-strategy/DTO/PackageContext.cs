// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using PackIt.Models.Package;

    /// <summary>   A package context. </summary>
    public class PackageContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PackageContext" /> class.
        /// </summary>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        public PackageContext(DbContextOptions<PackageContext> options)
            : base(options)
        {
        }

        /// <summary>   Gets or sets the packages. </summary>
        ///
        /// <value> The packages. </value>
        public DbSet<DtoPackage.DtoPackage> Packages { get; set; }

        /// <summary>   Gets the packages. </summary>
        ///
        /// <returns>   The packages. </returns>
        public List<Package> GetPackages()
        {
            List<Package> ret = new List<Package>();

            foreach (DtoPackage.DtoPackage item in this.Packages)
            {
                ret.Add(PackageMapper.Convert(item));
            }

            return ret;
        }

        /// <summary>   Adds a package. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddPackage(Package item)
        {
            DtoPackage.DtoPackage dto = PackageMapper.Convert(item);
            this.Packages.Add(dto);
        }

        /// <summary>   Searches for the first package. </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>   The found package. </returns>
        public Package FindPackage(string key)
        {
            DtoPackage.DtoPackage dto = this.Packages.Find(key);
            Package ret = dto == null ? null : PackageMapper.Convert(dto);

            return ret;
        }

        /// <summary>   Removes the package described by key. </summary>
        ///
        /// <param name="key">  The key. </param>
        public void RemovePackage(string key)
        {
            DtoPackage.DtoPackage entity = this.Packages.Find(key);
            this.Packages.Remove(entity);
        }

        /// <summary>   Updates the package described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdatePackage(Package item)
        {
            DtoPackage.DtoPackage entity = this.Packages.Find(item.Id);
            DtoPackage.DtoPackage dto = PackageMapper.Convert(item);
            this.Packages.Remove(entity);
            this.SaveChanges();
            this.Packages.Add(dto);
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
            modelBuilder.Entity<DtoPackage.DtoPackage>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<DtoPackage.DtoCosting>()
                .HasKey(c => new { c.PackageId, c.RequiredQuantity });
            modelBuilder.Entity<DtoPackage.DtoStage>()
                .HasKey(c => new { c.PackageId, c.Level });
            modelBuilder.Entity<DtoPackage.DtoLimit>()
                .HasKey(c => new { c.PackageId, c.StageLevel, c.Index });
            modelBuilder.Entity<DtoPackage.DtoResult>()
                .HasKey(c => new { c.PackageId, c.StageLevel, c.Index });
            modelBuilder.Entity<DtoPackage.DtoLayer>()
                .HasKey(c => new { c.PackageId, c.StageLevel, c.ResultIndex, c.Index });
            modelBuilder.Entity<DtoPackage.DtoCollation>()
                .HasKey(c => new { c.PackageId, c.StageLevel, c.ResultIndex, c.LayerIndex, c.Index });
            modelBuilder.Entity<DtoPackage.DtoMaterial>()
                .HasKey(c => new { c.PackageId, c.StageLevel, c.ResultIndex, c.Index });
            modelBuilder.Entity<DtoPackage.DtoSection>()
                .HasKey(c => new { c.PackageId, c.StageLevel, c.ResultIndex, c.Index });
        }
    }
}
