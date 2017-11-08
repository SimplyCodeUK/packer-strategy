// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PackIt.Pack;

    /// <summary> A pack context. </summary>
    public class PackContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PackContext" /> class.
        /// </summary>
        ///
        /// <param name="options"> Options for controlling the operation. </param>
        public PackContext(DbContextOptions<PackContext> options)
            : base(options)
        {
        }

        /// <summary> Gets the packs. </summary>
        ///
        /// <value> The packs. </value>
        public DbSet<DtoPack.DtoPack> Packs { get; private set; }

        /// <summary> Gets the packs. </summary>
        ///
        /// <returns> The packs. </returns>
        public IList<Pack> GetPacks()
        {
            var ret = new List<Pack>();
            var query = this.Packs
                .Include(p => p.Costings)
                .Include(p => p.Stages)
                .Include(p => p.Stages).ThenInclude(s => s.Limits)
                .Include(p => p.Stages).ThenInclude(s => s.Results)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Layers)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Layers).ThenInclude(l => l.Collations)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Materials)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Materials).ThenInclude(m => m.DatabaseMaterials)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Sections);

            foreach (DtoPack.DtoPack item in query)
            {
                ret.Add(PackMapper.Convert(item));
            }

            return ret;
        }

        /// <summary> Adds a pack. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void AddPack(Pack item)
        {
            DtoPack.DtoPack dto = PackMapper.Convert(item);
            this.Packs.Add(dto);
        }

        /// <summary> Searches for the first pack. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> The found pack. </returns>
        public Pack FindPack(string key)
        {
            try
            {
                var query = this.Packs
                    .Include(p => p.Costings)
                    .Include(p => p.Stages)
                    .Include(p => p.Stages).ThenInclude(s => s.Limits)
                    .Include(p => p.Stages).ThenInclude(s => s.Results)
                    .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Layers)
                    .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Layers).ThenInclude(l => l.Collations)
                    .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Materials)
                    .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Materials).ThenInclude(m => m.DatabaseMaterials)
                    .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Sections)
                    .SingleAsync(p => p.PackId == key);

                query.Wait();
                return PackMapper.Convert(query.Result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Removes the pack described by key. </summary>
        ///
        /// <param name="key"> The key. </param>
        public void RemovePack(string key)
        {
            DtoPack.DtoPack entity = this.Packs.Find(key);
            this.Packs.Remove(entity);
        }

        /// <summary> Updates the pack described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public void UpdatePack(Pack item)
        {
            DtoPack.DtoPack entity = this.Packs.Find(item.PackId);
            DtoPack.DtoPack dto = PackMapper.Convert(item);
            this.Packs.Remove(entity);
            this.SaveChanges();
            this.Packs.Add(dto);
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

            Configure(modelBuilder.Entity<DtoPack.DtoPack>());
            Configure(modelBuilder.Entity<DtoPack.DtoCosting>());
            Configure(modelBuilder.Entity<DtoPack.DtoStage>());
            Configure(modelBuilder.Entity<DtoPack.DtoLimit>());
            Configure(modelBuilder.Entity<DtoPack.DtoResult>());
            Configure(modelBuilder.Entity<DtoPack.DtoLayer>());
            Configure(modelBuilder.Entity<DtoPack.DtoCollation>());
            Configure(modelBuilder.Entity<DtoPack.DtoMaterial>());
            Configure(modelBuilder.Entity<DtoPack.DtoDatabaseMaterial>());
            Configure(modelBuilder.Entity<DtoPack.DtoSection>());
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoPack> builder)
        {
            builder.ToTable("DtoPack");
            builder.HasKey(p => p.PackId);
            builder
                .HasMany(p => p.Costings)
                .WithOne()
                .HasForeignKey(c => new { c.PackId });
            builder
                .HasMany(p => p.Stages)
                .WithOne()
                .HasForeignKey(s => new { s.PackId });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoCosting> builder)
        {
            builder.ToTable("DtoCosting");
            builder.HasKey(c => new { c.PackId, c.RequiredQuantity });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoStage> builder)
        {
            builder.ToTable("DtoStage");
            builder.HasKey(s => new { s.PackId, s.StageLevel });
            builder
                .HasMany(s => s.Limits)
                .WithOne()
                .HasForeignKey(l => new { l.PackId, l.StageLevel });
            builder
                .HasMany(s => s.Results)
                .WithOne()
                .HasForeignKey(r => new { r.PackId, r.StageLevel });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoLimit> builder)
        {
            builder.ToTable("DtoLimit");
            builder.HasKey(l => new { l.PackId, l.StageLevel, l.LimitIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoResult> builder)
        {
            builder.ToTable("DtoResult");
            builder.HasKey(r => new { r.PackId, r.StageLevel, r.ResultIndex });
            builder
                .HasMany(r => r.Layers)
                .WithOne()
                .HasForeignKey(l => new { l.PackId, l.StageLevel, l.ResultIndex });
            builder
                .HasMany(r => r.Materials)
                .WithOne()
                .HasForeignKey(m => new { m.PackId, m.StageLevel, m.ResultIndex });
            builder
                .HasMany(r => r.Sections)
                .WithOne()
                .HasForeignKey(s => new { s.PackId, s.StageLevel, s.ResultIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoLayer> builder)
        {
            builder.ToTable("DtoLayer");
            builder.HasKey(l => new { l.PackId, l.StageLevel, l.ResultIndex, l.LayerIndex });
            builder
                .HasMany(l => l.Collations)
                .WithOne()
                .HasForeignKey(c => new { c.PackId, c.StageLevel, c.ResultIndex, c.LayerIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoCollation> builder)
        {
            builder.ToTable("DtoCollation");
            builder.HasKey(c => new { c.PackId, c.StageLevel, c.ResultIndex, c.LayerIndex, c.CollationIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoMaterial> builder)
        {
            builder.ToTable("DtoMaterial");
            builder.HasKey(m => new { m.PackId, m.StageLevel, m.ResultIndex, m.MaterialIndex });
            builder
                .HasMany(m => m.DatabaseMaterials)
                .WithOne()
                .HasForeignKey(d => new { d.PackId, d.StageLevel, d.ResultIndex, d.MaterialIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoDatabaseMaterial> builder)
        {
            builder.ToTable("DtoDatabaseMaterial");
            builder.HasKey(d => new { d.PackId, d.StageLevel, d.ResultIndex, d.MaterialIndex, d.DatabaseMaterialIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="builder">The builder.</param>
        private static void Configure(EntityTypeBuilder<DtoPack.DtoSection> builder)
        {
            builder.ToTable("DtoSection");
            builder.HasKey(s => new { s.PackId, s.StageLevel, s.ResultIndex, s.SectionIndex });
        }
    }
}
