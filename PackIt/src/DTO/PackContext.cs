// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.DTO
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PackItLib.Pack;

    /// <summary> A pack context. </summary>
    ///
    /// <seealso cref="T:PackItLib.DTO.PackItContext{TData, TDtoData, TMapper}"/>
    /// <remarks>
    /// Initialises a new instance of the <see cref="PackContext" /> class.
    /// </remarks>
    ///
    /// <param name="options"> Options for controlling the operation. </param>
    public class PackContext(DbContextOptions<PackContext> options) : PackItLib.DTO.PackItContext<Pack, PackItLib.DTO.DtoPack.DtoPack, PackMapper>(options)
    {
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
            Configure<PackItLib.DTO.DtoPack.DtoCosting>(modelBuilder, "DtoCosting", k => new { k.PackId, k.RequiredQuantity });
            ConfigureDtoStage(modelBuilder);
            Configure<PackItLib.DTO.DtoPack.DtoLimit>(modelBuilder, "DtoLimit", k => new { k.PackId, k.StageLevel, k.LimitIndex });
            ConfigureDtoResult(modelBuilder);
            ConfigureDtoLayer(modelBuilder);
            Configure<PackItLib.DTO.DtoPack.DtoCollation>(modelBuilder, "DtoCollation", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.LayerIndex, k.CollationIndex });
            ConfigureDtoMaterial(modelBuilder);
            Configure<PackItLib.DTO.DtoPack.DtoSection>(modelBuilder, "DtoSection", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.SectionIndex });
            Configure<PackItLib.DTO.DtoPack.DtoDatabaseMaterial>(modelBuilder, "DtoDatabaseMaterial", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.MaterialIndex, k.DatabaseMaterialIndex });
        }

        /// <summary>Construct default query.</summary>
        ///
        /// <returns> Query for list of packs. </returns>
        protected override IQueryable<PackItLib.DTO.DtoPack.DtoPack> ConstructQuery()
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

        /// <summary>Construct a find task.</summary>
        ///
        /// <param name="key"> The key to search for. </param>
        ///
        /// <returns> The find task. </returns>
        protected override System.Threading.Tasks.Task<PackItLib.DTO.DtoPack.DtoPack> ConstructFindTask(string key)
        {
            return this.ConstructQuery().SingleAsync(p => p.PackId == key);
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoPack(ModelBuilder modelBuilder)
        {
            var builder = Configure<PackItLib.DTO.DtoPack.DtoPack>(modelBuilder, "DtoPack", k => k.PackId);
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
            var builder = Configure<PackItLib.DTO.DtoPack.DtoStage>(modelBuilder, "DtoStage", k => new { k.PackId, k.StageLevel });
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
            var builder = Configure<PackItLib.DTO.DtoPack.DtoResult>(modelBuilder, "DtoResult", k => new { k.PackId, k.StageLevel, k.ResultIndex });
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
            Configure<PackItLib.DTO.DtoPack.DtoLayer>(modelBuilder, "DtoLayer", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.LayerIndex })
                .HasMany(l => l.Collations)
                .WithOne()
                .HasForeignKey(c => new { c.PackId, c.StageLevel, c.ResultIndex, c.LayerIndex });
        }

        /// <summary>Configures the specified builder.</summary>
        ///
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureDtoMaterial(ModelBuilder modelBuilder)
        {
            Configure<PackItLib.DTO.DtoPack.DtoMaterial>(modelBuilder, "DtoMaterial", k => new { k.PackId, k.StageLevel, k.ResultIndex, k.MaterialIndex })
                .HasMany(m => m.DatabaseMaterials)
                .WithOne()
                .HasForeignKey(d => new { d.PackId, d.StageLevel, d.ResultIndex, d.MaterialIndex });
        }
    }
}
