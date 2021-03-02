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
    using PackIt.Pack;

    /// <summary> A pack context. </summary>
    ///
    /// <seealso cref="T:PackIt.DTO.PackItContext{TData, TDtoData}"/>
    public class PackContext : PackItContext<Pack, DtoPack.DtoPack>
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
        /// <returns> The packs. </returns>
        public override IList<Pack> GetAll()
        {
            var ret = new List<Pack>();
            var query = ConstructQuery();

            foreach (var item in query)
            {
                ret.Add(PackMapper.Convert(item));
            }

            return ret;
        }

        /// <summary> Adds a pack. </summary>
        ///
        /// <param name="item"> The item. </param>
        public override void Add(Pack item)
        {
            var dto = PackMapper.Convert(item);
            this.Resources.Add(dto);
        }

        /// <summary> Searches for the first pack. </summary>
        ///
        /// <param name="key"> The key. </param>
        ///
        /// <returns> The found pack. </returns>
        public override Pack Find(string key)
        {
            try
            {
                var query = ConstructQuery().SingleAsync(p => p.PackId == key);

                query.Wait();
                return PackMapper.Convert(query.Result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Updates the pack described by item. </summary>
        ///
        /// <param name="item"> The item. </param>
        public override void Update(Pack item)
        {
            var entity = this.Resources.Find(item.PackId);
            var dto = PackMapper.Convert(item);
            this.Resources.Remove(entity);
            this.SaveChanges();
            this.Resources.Add(dto);
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

            ConfigureDtoPack(modelBuilder);
            Configure<DtoPack.DtoCosting>(modelBuilder, "DtoCosting", k => new { k.PackId, k.RequiredQuantity });
            ConfigureDtoStage(modelBuilder);
            Configure<DtoPack.DtoLimit>(modelBuilder, "DtoLimit", k => new { k.PackId, k.StageLevel, k.LimitIndex });
            ConfigureDtoResult(modelBuilder);
            ConfigureDtoLayer(modelBuilder);
            Configure<DtoPack.DtoCollation>(modelBuilder, "DtoCollation", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.LayerIndex, k.CollationIndex });
            ConfigureDtoMaterial(modelBuilder);
            Configure<DtoPack.DtoSection>(modelBuilder, "DtoSection", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.SectionIndex });
            Configure<DtoPack.DtoDatabaseMaterial>(modelBuilder, "DtoDatabaseMaterial", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.MaterialIndex, k.DatabaseMaterialIndex });
        }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of packs. </returns>
        protected IQueryable<DtoPack.DtoPack> ConstructQuery()
        {
            var query = this.Resources
                .Include(p => p.Costings)
                .Include(p => p.Stages)
                .Include(p => p.Stages).ThenInclude(s => s.Limits)
                .Include(p => p.Stages).ThenInclude(s => s.Results)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Layers)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Layers).ThenInclude(l => l.Collations)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Materials)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Materials).ThenInclude(m => m.DatabaseMaterials)
                .Include(p => p.Stages).ThenInclude(s => s.Results).ThenInclude(r => r.Sections);

            return query;
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoPack(ModelBuilder modelBuilder)
        {
            var builder = Configure<DtoPack.DtoPack>(modelBuilder, "DtoPack", k => k.PackId);
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
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoStage(ModelBuilder modelBuilder)
        {
            var builder = Configure<DtoPack.DtoStage>(modelBuilder, "DtoStage", k => new { k.PackId, k.StageLevel });
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
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoResult(ModelBuilder modelBuilder)
        {
            var builder = Configure<DtoPack.DtoResult>(modelBuilder, "DtoResult", k => new { k.PackId, k.StageLevel, k.ResultIndex });
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
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoLayer(ModelBuilder modelBuilder)
        {
            Configure<DtoPack.DtoLayer>(modelBuilder, "DtoLayer", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.LayerIndex })
                .HasMany(l => l.Collations)
                .WithOne()
                .HasForeignKey(c => new { c.PackId, c.StageLevel, c.ResultIndex, c.LayerIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoMaterial(ModelBuilder modelBuilder)
        {
            Configure<DtoPack.DtoMaterial>(modelBuilder, "DtoMaterial", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.MaterialIndex })
                .HasMany(m => m.DatabaseMaterials)
                .WithOne()
                .HasForeignKey(d => new { d.PackId, d.StageLevel, d.ResultIndex, d.MaterialIndex });
        }
    }
}
