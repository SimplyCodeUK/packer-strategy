// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PackIt.Material;

    /// <summary> A material context. </summary>
    public class MaterialContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialContext" /> class.
        /// </summary>
        ///
        /// <param name="options"> Options for controlling the operation. </param>
        public MaterialContext(DbContextOptions<MaterialContext> options)
            : base(options)

        {
        }

        /// <summary> Gets the materials. </summary>
        ///
        /// <value> The materials. </value>
        public DbSet<DtoMaterial.DtoMaterial> Materials { get; private set; }

        /// <summary> Gets the materials. </summary>
        ///
        /// <returns> The materials. </returns>
        public IList<Material> GetMaterials()
        {
            var ret = new List<Material>();
            var query = ConstructQuery();

            foreach (DtoMaterial.DtoMaterial item in query)
            {
                ret.Add(MaterialMapper.Convert(item));
            }

            return ret;
        }

        /// <summary> Adds a material. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddMaterial(Material item)
        {
            DtoMaterial.DtoMaterial dto = MaterialMapper.Convert(item);
            this.Materials.Add(dto);
        }

        /// <summary> Searches for the first material. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> The found material. </returns>
        public Material FindMaterial(string key)
        {
            try
            {
                var query = ConstructQuery()
                    .SingleAsync(p => p.MaterialId == key);

                query.Wait();
                return MaterialMapper.Convert(query.Result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Removes the material. </summary>
        ///
        /// <param name="key"> The key. </param>
        public void RemoveMaterial(string key)
        {
            DtoMaterial.DtoMaterial entity = this.Materials.Find(key);
            this.Materials.Remove(entity);
        }

        /// <summary> Updates the material described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdateMaterial(Material item)
        {
            DtoMaterial.DtoMaterial entity = this.Materials.Find(item.MaterialId);
            DtoMaterial.DtoMaterial dto = MaterialMapper.Convert(item);
            this.Materials.Remove(entity);
            this.SaveChanges();
            this.Materials.Add(dto);
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
            base.OnModelCreating(modelBuilder);

            Configure(modelBuilder.Entity<DtoMaterial.DtoMaterial>());
            Configure(modelBuilder.Entity<DtoMaterial.DtoCosting>());
            Configure(modelBuilder.Entity<DtoMaterial.DtoLayer>());
            Configure(modelBuilder.Entity<DtoMaterial.DtoSection>());
            Configure(modelBuilder.Entity<DtoMaterial.DtoCollation>());
            Configure(modelBuilder.Entity<DtoMaterial.DtoPalletDeck>());
            Configure(modelBuilder.Entity<DtoMaterial.DtoPlank>());
        }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of materials. </returns>
        private IQueryable<DtoMaterial.DtoMaterial> ConstructQuery()
        {
            var query = this.Materials
                .Include(m => m.Costings)
                .Include(m => m.Layers)
                .Include(m => m.Layers).ThenInclude(l => l.Collations)
                .Include(m => m.PalletDecks)
                .Include(m => m.PalletDecks).ThenInclude(p => p.Planks)
                .Include(m => m.Sections);

            return query;
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoMaterial.DtoMaterial> builder)
        {
            builder.ToTable("DtoMaterial");
            builder.HasKey(m => new { m.MaterialId });
            builder
                .HasMany(m => m.Costings)
                .WithOne()
                .HasForeignKey(c => new { c.MaterialId });
            builder
                .HasMany(m => m.Layers)
                .WithOne()
                .HasForeignKey(l => new { l.MaterialId });
            builder
                .HasMany(m => m.PalletDecks)
                .WithOne()
                .HasForeignKey(p => new { p.MaterialId });
            builder
                .HasMany(m => m.Sections)
                .WithOne()
                .HasForeignKey(s => new { s.MaterialId });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoMaterial.DtoCosting> builder)
        {
            builder.ToTable("DtoCosting");
            builder.HasKey(c => new { c.MaterialId, c.Quantity });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoMaterial.DtoLayer> builder)
        {
            builder.ToTable("DtoLayer");
            builder.HasKey(l => new { l.MaterialId, l.LayerIndex });
            builder
                .HasMany(m => m.Collations)
                .WithOne()
                .HasForeignKey(c => new { c.MaterialId, c.LayerIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoMaterial.DtoSection> builder)
        {
            builder.ToTable("DtoSection");
            builder.HasKey(s => new { s.MaterialId, s.SectionIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoMaterial.DtoCollation> builder)
        {
            builder.ToTable("DtoCollation");
            builder.HasKey(c => new { c.MaterialId, c.LayerIndex, c.CollationIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoMaterial.DtoPalletDeck> builder)
        {
            builder.ToTable("DtoPalletDeck");
            builder.HasKey(p => new { p.MaterialId, p.PalletDeckIndex });
            builder
                .HasMany(p => p.Planks)
                .WithOne()
                .HasForeignKey(p => new { p.MaterialId, p.PalletDeckIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoMaterial.DtoPlank> builder)
        {
            builder.ToTable("DtoPlank");
            builder.HasKey(p => new { p.MaterialId, p.PalletDeckIndex, p.PlankIndex });
        }
    }
}
